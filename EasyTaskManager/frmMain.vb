Imports System.ServiceProcess
Imports System.Threading
Imports Microsoft.VisualBasic.Devices
Imports System.Management
Imports System.Globalization
Imports System.Reflection
Imports EasyTaskManager

Public Class frmMain

#Region "Variables"
    ''
    Private _interval As Integer = 10000 'Indicate of the interval of timer

    Private _rm As Resources.ResourceManager
    Private _cultureInfo As CultureInfo

    Private _threadProcess As Thread = Nothing
    Private _threadServices As Thread = Nothing

    Private _tmrProcess As System.Windows.Forms.Timer = Nothing
    Private _tmrServices As System.Windows.Forms.Timer = Nothing

    Private Delegate Sub del_Process(pId As String, pName As String, pDescription As String, pUsername As String, pMemory As String, pStatus As String, pPriority As String)
    Private Delegate Sub del_Services(pName As String, pDescription As String, pMachine As String, pStatus As String)
    Private Delegate Sub del_UtilizationCPU(pUtilization As String)
    Private Delegate Sub del_UtilizationRAM(pUtilization As String)
    Private Delegate Sub del_NumberProcess(pProcess As String)
    Private Delegate Sub del_NumberSubProcess(pProcess As String)

#End Region

    Private Const CPU As Integer = 0
    Private Const PROCESSOR As Integer = 1
    Private Const MEMORY As Integer = 2

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        Initialize()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub


#Region "Methods Private"

    Private Sub Initialize()
        Try
            _cultureInfo = Application.CurrentCulture
            ConfigureCulture()

            LoadedResources()

            ConfigureStatusStrip()
            ConfigureListView()
            ConfigureListViewServices()

            'Customized timers
            'Process
            _tmrProcess = New System.Windows.Forms.Timer
            _tmrProcess.Interval = _interval

            'Services 
            _tmrServices = New System.Windows.Forms.Timer
            _tmrServices.Interval = _interval

            'Handler timer
            AddHandler _tmrProcess.Tick, AddressOf TmrProcess_Tick
            AddHandler _tmrServices.Tick, AddressOf TmrServices_Tick


            'Load process
            _threadProcess = New Thread(New ThreadStart(AddressOf LoadProcess))
            _threadProcess.Name = "proEasyTaskManager"
            _threadProcess.IsBackground = True
            _threadProcess.Start()
            ''Load services
            _threadServices = New Thread(New ThreadStart(AddressOf LoadServices))
            _threadServices.Name = "servEasyTaskManager"
            _threadServices.IsBackground = True
            _threadServices.Start()

            _tmrProcess.Start()
            _tmrServices.Start()

            Dim propName As String = "BatteryLifePercent"
            Dim t As Type = GetType(System.Windows.Forms.PowerStatus)
            Dim pi As PropertyInfo() = t.GetProperties
            Dim prop As PropertyInfo
            For i As Integer = 0 To pi.Length - 1
                If pi(i).Name = propName Then
                    prop = pi(i)
                    Exit For
                End If
            Next

            ' Environment.Is64BitProcess

            'Dim powerLevel As Long = prop.GetValue(SystemInformation.PowerStatus, Nothing)
            'Dim avgCounter64Sample As PerformanceCounter = New PerformanceCounter("AverageCounter64SampleCategory", "AverageCounter64Sample", False)
            'Dim avgCounter64SampleBase As PerformanceCounter = New PerformanceCounter("AverageCounter64SampleCategory", "AverageCounter64Sample", False)

            'avgCounter64Sample.RawValue = 0
            'avgCounter64SampleBase.RawValue = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try

    End Sub

    ''' <summary>
    ''' Configure the status bar
    ''' </summary>
    Private Sub ConfigureStatusStrip()
        ssEasyTaskManager.Items.Add("CPU")
        ssEasyTaskManager.Items.Add("Memory")
        ssEasyTaskManager.Items.Add("Process")

        ssEasyTaskManager.Items(CPU).Text = My.Resources.EasyTaskManager.CPU
        ssEasyTaskManager.Items(MEMORY).Text = My.Resources.EasyTaskManager.Memory
        ssEasyTaskManager.Items(PROCESSOR).Text = My.Resources.EasyTaskManager.Process
    End Sub

    ''' <summary>
    ''' Loaded resources the system
    ''' </summary>
    Private Sub LoadedResources()
        Me.Text = My.Resources.EasyTaskManager.ApplicationName

        tcEasyTaskManager.TabPages(0).Text = My.Resources.EasyTaskManager.GeneralInformation
        tcEasyTaskManager.TabPages(1).Text = My.Resources.EasyTaskManager.Process
        tcEasyTaskManager.TabPages(2).Text = My.Resources.EasyTaskManager.Services

        lblTitleCPU.Text = My.Resources.EasyTaskManager.CPU
        lblTitleMemory.Text = My.Resources.EasyTaskManager.Memory
        lblTitleProcess.Text = My.Resources.EasyTaskManager.Process
        lblTitleSubProcess.Text = My.Resources.EasyTaskManager.Subprocess

        tsmnuiFile.Text = My.Resources.EasyTaskManager.File
        tsmnuiNewTask.Text = My.Resources.EasyTaskManager.NewTask
        tsmnuiClose.Text = My.Resources.EasyTaskManager.Close

    End Sub

    ''' <summary>
    ''' Configure the culture
    ''' </summary>
    Private Sub ConfigureCulture()
        Threading.Thread.CurrentThread.CurrentUICulture = _cultureInfo

        If _cultureInfo.Name.Contains("es-CR") Then
            _rm = New Resources.ResourceManager("SeguridadWF.My.Resources.IntegraSeguridad.es-CR", Assembly.GetExecutingAssembly())
        Else
            _rm = New Resources.ResourceManager("SeguridadWF.My.Resources.IntegraSeguridad", Assembly.GetExecutingAssembly())
        End If
    End Sub

    ''' <summary>
    ''' Configure Listview of the processes
    ''' </summary>
    Private Sub ConfigureListView()
        lvProcess.FullRowSelect = True
        lvProcess.MultiSelect = False

        lvProcess.Columns.Clear()
        lvProcess.Items.Clear()

        lvProcess.Columns.Add(My.Resources.EasyTaskManager.IdProcess, 100)
        lvProcess.Columns.Add(My.Resources.EasyTaskManager.ProcessName, 300)
        lvProcess.Columns.Add(My.Resources.EasyTaskManager.Description, 350)
        lvProcess.Columns.Add(My.Resources.EasyTaskManager.Username, 100)
        lvProcess.Columns.Add(My.Resources.EasyTaskManager.Memory, 100)
        lvProcess.Columns.Add(My.Resources.EasyTaskManager.Status, 100)
        lvProcess.Columns.Add(My.Resources.EasyTaskManager.Priority, 100)

        lvProcess.Columns(4).TextAlign = HorizontalAlignment.Right

        lvProcess.View = View.Details
    End Sub

    ''' <summary>
    ''' Configure Listview of the services
    ''' </summary>
    Private Sub ConfigureListViewServices()
        lvServices.Columns.Clear()
        lvServices.Items.Clear()

        lvServices.Columns.Add(My.Resources.EasyTaskManager.IdProcess, 200)
        lvServices.Columns.Add("Name", 350)
        lvServices.Columns.Add("Machine", 100)
        lvServices.Columns.Add(My.Resources.EasyTaskManager.Status, 100)

        lvServices.View = View.Details
    End Sub

    ''' <summary>
    ''' Loading current processes 
    ''' </summary>
    Private Sub LoadProcess()

        Dim listProcess As List(Of Process) = Process.GetProcesses.OrderBy(Function(p) p.ProcessName).ToList
            Dim lvi As ListViewItem = Nothing
            Dim statusProcess As Boolean = Nothing
            Dim proc As ProcessStartInfo = Nothing
            Dim mo As ManagementObject
            Dim methodResult As ManagementBaseObject
            Dim processOwner As String = "Not Available"
            Dim priority As String
            Dim fv As FileVersionInfo = Nothing
        Dim description As String

        Dim subProcess As Long = 0

        For Each p As Process In listProcess
            'lvi = New ListViewItem()
            'lvi.Name = p.Id
            'lvi.Text = p.Id
            'lvi.SubItems.Add(p.ProcessName)
            'lvi.SubItems.Add(p.StartInfo.UserName)
            'lvi.SubItems.Add(p.VirtualMemorySize.ToString)
            'lvi.SubItems.Add("Running")

            'lvProcess.Items.Add(lvi)
            'mo = New ManagementObject("root\CIMV2", "Win32_Process.Handle=" & p.Id, Nothing)
            'methodResult = mo.InvokeMethod("GetOwner", Nothing, Nothing)

            'If CInt(methodResult("ReturnValue")) = 0 Then
            '    processOwner = String.Format("{0}\{1}", methodResult("Domain").ToString, methodResult("User").ToString)
            'End If

            'processOwner = p.StartInfo.UserName

            'Select Case p.PriorityClass
            '    Case ProcessPriorityClass.Idle
            '        priority = My.Resources.EasyTaskManager.Idle
            '    Case ProcessPriorityClass.Normal
            '        priority = My.Resources.EasyTaskManager.Normal
            '    Case ProcessPriorityClass.High
            '        priority = My.Resources.EasyTaskManager.High
            '    Case ProcessPriorityClass.RealTime
            '        priority = My.Resources.EasyTaskManager.RealTime
            'End Select
            'proce = Process.GetProcessById(p.Id)
            '
            Try
                'path = p.Modules(0).FileName
                description = FileVersionInfo.GetVersionInfo(p.Modules(0).FileName).FileDescription
                priority = p.PriorityClass.ToString
            Catch ex As Exception
                description = ""
            End Try

            subProcess += p.Threads.Count

            ListViewAddItemProcess(p.Id, p.ProcessName, description, p.StartInfo.UserName, String.Format("{0} K", Math.Round((p.PrivateMemorySize64 / 1024), 0).ToString("0,000")), "Running", priority)

        Next

        NumberProcess(listProcess.Count)
        NumberProcessStatus(listProcess.Count)

        NumberSubProcess(subProcess)

        UtilizationCPU(String.Format("{0} %", UsedCPU.ToString("#,###")))
        UtilizationCPUStatus(String.Format("{0} %", UsedCPU.ToString("#,###")))

        UtilizationRAM(String.Format("{0} GB", UsedMemory.ToString("#,#")))
        UtilizationRamStatus(String.Format("{0} GB", UsedMemory.ToString("#,#")))

        _threadProcess.Abort()
            While _threadProcess.IsAlive

            End While

    End Sub

    Private Sub LoadServices()
        Dim listServices As List(Of ServiceController) = ServiceController.GetServices.OrderBy(Function(sc) sc.DisplayName).ToList
        Dim lviServices As ListViewItem = Nothing
        Dim statusService As String = Nothing

        For Each s As ServiceController In listServices
            lviServices = New ListViewItem()
            lviServices.Name = s.ServiceName
            lviServices.Text = s.ServiceName

            lviServices.SubItems.Add(s.DisplayName)
            lviServices.SubItems.Add(s.MachineName)
            lviServices.SubItems.Add(s.Status.ToString)

            'Select Case s.Status
            '    Case ServiceControllerStatus.Running
            '        statusService = "Running"
            '    Case ServiceControllerStatus.Paused
            '        statusService = "Paused"
            '    Case ServiceControllerStatus.Stopped
            '        statusService = "Stopped"
            '    Case ServiceControllerStatus.ContinuePending
            '        statusService = "Continue Pending"
            'End Select

            'lviServices.SubItems.Add(statusService)

            'lvServices.Items.Add(lviServices)

            ListViewAddItemServices(s.DisplayName, s.DisplayName, s.MachineName, s.Status.ToString)
        Next
        _threadServices.Abort()
        While _threadServices.IsAlive

        End While
    End Sub

    Private Function UsedCPU() As Decimal
        Dim percent As Decimal = 0
        'Dim processors As New Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor")
        'For Each i As ManagementObject In processors.Get
        '    percent = percent + i.Options.Timeout.Milliseconds
        'Next

        Dim cpuCounter As New PerformanceCounter
        cpuCounter.CategoryName = "Processor"
        cpuCounter.CounterName = "% Processor Time"
        cpuCounter.InstanceName = "_Total"

        cpuCounter.NextValue()
        System.Threading.Thread.Sleep(100)
        percent = cpuCounter.NextValue

        Return percent
    End Function

    Private Function UsedMemory() As Decimal
        Dim percent As Decimal = 0

        Dim ramCounter As New PerformanceCounter
        ramCounter.CategoryName = "Memory"
        ramCounter.CounterName = "Available MBytes"
        ' cpuCounter.InstanceName = "_Total"

        ramCounter.NextValue()
        System.Threading.Thread.Sleep(100)
        percent = ramCounter.NextValue

        Return percent
    End Function

    Private Sub ListViewAddItemProcess(pId As String, pName As String, pDescrpition As String, pUsername As String, pMemory As String, pStatus As String, pPriority As String)
        If lvProcess.InvokeRequired Then
            Dim d As New del_Process(AddressOf ListViewAddItemProcess)
            Me.lvProcess.BeginInvoke(d, {pId, pName, pDescrpition, pUsername, pMemory, pStatus, pPriority})
        Else
            lvProcess.Items.Add(New ListViewItem(New String() {pId, pName, pDescrpition, pUsername, pMemory, pStatus, pPriority}))
        End If
    End Sub

    Private Sub ListViewAddItemServices(pName As String, pDescription As String, pMachine As String, pStatus As String)
        If lvServices.InvokeRequired Then
            Dim d As New del_Services(AddressOf ListViewAddItemServices)
            Me.lvServices.BeginInvoke(d, {pName, pDescription, pMachine, pStatus})
        Else
            lvServices.Items.Add(New ListViewItem(New String() {pName, pDescription, pMachine, pStatus}))
        End If
    End Sub

    Private Sub UtilizationCPU(pUtilization As String)
        If lblCPU.InvokeRequired Then
            Dim d As New del_UtilizationCPU(AddressOf UtilizationCPU)
            lblCPU.BeginInvoke(d, pUtilization)
        Else
            lblCPU.Text = pUtilization
        End If
    End Sub

    Private Sub UtilizationRAM(pUtilization As String)
        If lblCPU.InvokeRequired Then
            Dim d As New del_UtilizationRAM(AddressOf UtilizationRAM)
            lblMemory.BeginInvoke(d, pUtilization)
        Else
            lblMemory.Text = pUtilization
        End If
    End Sub

    Private Sub NumberProcess(pProcess As String)
        If lblProcess.InvokeRequired Then
            Dim d As New del_NumberProcess(AddressOf NumberProcess)
            lblProcess.BeginInvoke(d, pProcess)
        Else
            lblProcess.Text = pProcess
        End If
    End Sub

    Private Sub NumberSubProcess(pProcess As String)
        If lblSubProcess.InvokeRequired Then
            Dim d As New del_NumberSubProcess(AddressOf NumberSubProcess)
            lblSubProcess.BeginInvoke(d, pProcess)
        Else
            lblSubProcess.Text = pProcess
        End If
    End Sub

    Private Sub UtilizationCPUStatus(pUtilization As String)
        If ssEasyTaskManager.InvokeRequired Then
            Dim d As New del_NumberProcess(AddressOf UtilizationCPUStatus)
            ssEasyTaskManager.BeginInvoke(d, New Object() {pUtilization})
        Else
            ssEasyTaskManager.Items(CPU).Text = String.Format("{0} {1}", My.Resources.EasyTaskManager.CPU, pUtilization)
        End If
    End Sub

    Private Sub UtilizationRamStatus(pUtilization As String)
        If ssEasyTaskManager.InvokeRequired Then
            Dim d As New del_UtilizationRAM(AddressOf UtilizationRamStatus)
            ssEasyTaskManager.BeginInvoke(d, New Object() {pUtilization})
        Else
            ssEasyTaskManager.Items(MEMORY).Text = String.Format("{0} {1}", My.Resources.EasyTaskManager.Memory, pUtilization)
        End If
    End Sub

    Private Sub NumberProcessStatus(pProcess As String)
        If ssEasyTaskManager.InvokeRequired Then
            Dim d As New del_NumberProcess(AddressOf NumberProcessStatus)
            ssEasyTaskManager.BeginInvoke(d, New Object() {pProcess})
        Else
            ssEasyTaskManager.Items(PROCESSOR).Text = String.Format("{0} {1}", My.Resources.EasyTaskManager.Process, pProcess)
        End If
    End Sub

    Private Function ConfigureContextMenuEasyTaskManager(eType As eType) As ContextMenuStrip
        Dim cms As ContextMenuStrip = cmsEasyTaskManager
        cms.Items.Clear()

        Select Case eType
            Case eType.GeneralInformation
            Case eType.Processes
                Dim cmsChangePriorityProcess As ToolStripMenuItem = cms.Items.Add(My.Resources.EasyTaskManager.ChangePriorityProcess)
                Dim cmsFinalizeTask As ToolStripItem = cms.Items.Add(My.Resources.EasyTaskManager.FinalizeTask)
                Dim cmsFinalizeTreeProcess As ToolStripItem = cms.Items.Add(My.Resources.EasyTaskManager.FinalizeTreeProcess)
                Dim cmsGotoServices As ToolStripItem = cms.Items.Add(My.Resources.EasyTaskManager.GotoServices)

                'Dim tsiPriorityNormal As ToolStripItem = cmsChangePriorityProcess(My.Resources.EasyTaskManager.PriorityNormal)
                cmsChangePriorityProcess.DropDownItems.Add(My.Resources.EasyTaskManager.PriorityNormal)
                cmsChangePriorityProcess.DropDownItems.Add(My.Resources.EasyTaskManager.PriorityHigh)
                cmsChangePriorityProcess.DropDownItems.Add(My.Resources.EasyTaskManager.PriorityLow)

                'tsiPriorityNormal.Owne = cmsChangePriorityProcess

                AddHandler cmsFinalizeTask.Click, AddressOf FinalizeTask_Click
                AddHandler cmsFinalizeTreeProcess.Click, AddressOf FinalizeTreeProcess_Click
                AddHandler cmsGotoServices.Click, AddressOf GotoServices_Click

                AddHandler cmsChangePriorityProcess.DropDownItemClicked, AddressOf ChangePriorityProcess_Click
            Case eType.Services
                Dim cmsFinalizeServices As ToolStripItem = cms.Items.Add(My.Resources.EasyTaskManager.StopServices)
                Dim cmsStartServices As ToolStripItem = cms.Items.Add(My.Resources.EasyTaskManager.StartServices)
                'Dim cmsGotoServices As ToolStripItem = cms.Items.Add(My.Resources.EasyTaskManager.GotoServices)

                ' Dim tsiPriorityNormal As ToolStripItem = cms.Items.AddRange(My.Resources.EasyTaskManager.PriorityNormal)

                'tsiPriorityNormal.Owne = cmsChangePriorityProcess

                AddHandler cmsFinalizeServices.Click, AddressOf FinalizeServices_Click
                AddHandler cmsStartServices.Click, AddressOf StartServices_Click
                'AddHandler cmsGotoServices.Click, AddressOf GotoServices_Click
        End Select
        Return cms
    End Function

#End Region

#Region "Events"

    Private Sub TmrServices_Tick(sender As Object, e As EventArgs)
        If Not _threadServices.IsAlive AndAlso (_threadServices.ThreadState = ThreadState.Stopped OrElse _threadServices.ThreadState = ThreadState.Unstarted) Then
            ''Load services
            _threadServices = New Thread(New ThreadStart(AddressOf LoadServices))
            _threadServices.Name = "servEasyTaskManager"
            _threadServices.IsBackground = True
            _threadServices.Start()
        End If
    End Sub

    Private Sub TmrProcess_Tick(sender As Object, e As EventArgs)
        If Not _threadProcess.IsAlive AndAlso (_threadProcess.ThreadState = ThreadState.Stopped OrElse _threadProcess.ThreadState = ThreadState.Unstarted) Then
            'Load process
            _threadProcess = New Thread(New ThreadStart(AddressOf LoadProcess))
            _threadProcess.Name = "proEasyTaskManager"
            _threadProcess.IsBackground = True
            _threadProcess.Start()
        End If
    End Sub

    Private Sub StartServices_Click(sender As Object, e As EventArgs)
        Try
            If lvServices.SelectedItems.Count > 0 Then
                Dim s As New ServiceController(lvServices.SelectedItems(0).Text)
                If s.Status = ServiceControllerStatus.Stopped Then
                    s.Start()
                    s.WaitForStatus(ServiceControllerStatus.Running)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub FinalizeServices_Click(sender As Object, e As EventArgs)
        If lvServices.SelectedItems.Count > 0 Then
            Dim s As New ServiceController(lvServices.SelectedItems(0).Text)
            If s.Status = ServiceControllerStatus.Running Or s.Status = ServiceControllerStatus.StartPending Then
                s.Stop()
                s.WaitForStatus(ServiceControllerStatus.Stopped)
            End If
        End If
    End Sub

    Private Sub GotoServices_Click(sender As Object, e As EventArgs)
        tpServices.Select()
        tcEasyTaskManager.SelectedIndex = 2
    End Sub

    Private Sub FinalizeTreeProcess_Click(sender As Object, e As EventArgs)
        If lvProcess.SelectedItems.Count > 0 Then
            Dim p As Process = Process.GetProcessById(lvProcess.SelectedItems(0).Text)
            p.Kill()
            p.WaitForExit()
        End If
    End Sub

    Private Sub ChangePriorityProcess_Click(sender As Object, e As ToolStripItemClickedEventArgs)
        If lvProcess.SelectedItems.Count > 0 Then
            Try
                Dim p As Process = Process.GetProcessById(lvProcess.SelectedItems(0).Text)
                Select Case e.ClickedItem.Text
                    Case My.Resources.EasyTaskManager.PriorityNormal
                        p.PriorityClass = ProcessPriorityClass.Normal
                    Case My.Resources.EasyTaskManager.PriorityLow
                        p.PriorityClass = ProcessPriorityClass.AboveNormal
                End Select
                'p.Start()
                cmsEasyTaskManager.Close()
                p.WaitForExit()
            Catch ex As Exception
                MessageBox.Show(ex.Message, Me.Text)
            End Try

        End If
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        _tmrProcess.Stop()
        _tmrServices.Stop()
        _threadProcess.Abort()
        _threadServices.Abort()

        _tmrProcess = Nothing
        _tmrServices = Nothing
        _threadProcess = Nothing
        _threadServices = Nothing

        GC.GetTotalMemory(True)
    End Sub

    Private Sub tcEasyTaskManager_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcEasyTaskManager.SelectedIndexChanged
        Dim typeTask As eType = Nothing
        Select Case tcEasyTaskManager.SelectedTab.Name
            Case "tpGeneralInformation"
            Case "tpProcesses"
                typeTask = eType.Processes
                lvProcess.ContextMenuStrip = ConfigureContextMenuEasyTaskManager(typeTask)
            Case "tpServices"
                typeTask = eType.Services
                lvServices.ContextMenuStrip = ConfigureContextMenuEasyTaskManager(typeTask)
        End Select
    End Sub

    Private Sub FinalizeTask_Click(sender As Object, e As EventArgs)
        If lvProcess.SelectedItems.Count > 0 Then
            Dim p As Process = Process.GetProcessById(lvProcess.SelectedItems(0).Text)
            p.Kill()
            p.WaitForExit()
        End If
    End Sub

    Private Sub tsmnuiNewTask_Click(sender As Object, e As EventArgs) Handles tsmnuiNewTask.Click
        Dim fNewProcess As New frmNewProcess
        frmNewProcess.ShowDialog()
    End Sub

    Private Sub lvProcess_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles lvProcess.DrawItem
        If Not (e.State = ListViewItemStates.Selected) = 0 Then
            e.Graphics.FillRectangle(Brushes.SkyBlue, e.Bounds)
            e.DrawFocusRectangle()
        End If
    End Sub


#End Region

End Class

Public Enum eType
    GeneralInformation = 1
    Processes = 2
    Services = 3
End Enum


