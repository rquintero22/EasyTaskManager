Imports System.ServiceProcess
Imports System.Threading
Imports Microsoft.VisualBasic.Devices
Imports System.Management
Imports System.Globalization
Imports System.Reflection
Imports EasyTaskManager

Public Class frmMain

#Region "Variables"

    Private _interval As Integer = 2000 'Indicate of the interval of timer

    Private _rm As Resources.ResourceManager
    Private _cultureInfo As CultureInfo

    Private _threadProcess As Thread = Nothing
    Private _threadServices As Thread = Nothing

    Private _tmrProcess As System.Windows.Forms.Timer = Nothing
    Private _tmrServices As System.Windows.Forms.Timer = Nothing

    Private Delegate Sub del_Process(pId As String, pName As String, pUsername As String, pMemory As String, pStatus As String)
    Private Delegate Sub del_Services(pName As String, pDescription As String, pMachine As String, pStatus As String)
    Private Delegate Sub del_UtilizationCPU(pUtilization As String)
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

    Private Sub Initialize()
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

        'LoadProcess()
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

    Private Sub TmrServices_Tick(sender As Object, e As EventArgs)
        If Not _threadServices.IsAlive AndAlso (_threadServices.ThreadState = ThreadState.Stopped OrElse _threadServices.ThreadState = ThreadState.Unstarted) Then

            'Load process
            _threadProcess = New Thread(New ThreadStart(AddressOf LoadProcess))
            _threadProcess.Name = "proEasyTaskManager"
            _threadProcess.IsBackground = True
            _threadProcess.Start()

        End If
    End Sub

    Private Sub TmrProcess_Tick(sender As Object, e As EventArgs)
        If Not _threadProcess.IsAlive AndAlso (_threadProcess.ThreadState = ThreadState.Stopped OrElse _threadProcess.ThreadState = ThreadState.Unstarted) Then
            ''Load services
            _threadServices = New Thread(New ThreadStart(AddressOf LoadServices))
            _threadServices.Name = "servEasyTaskManager"
            _threadServices.IsBackground = True
            _threadServices.Start()

        End If
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
        lvProcess.Columns.Clear()
        lvProcess.Items.Clear()

        lvProcess.Columns.Add(My.Resources.EasyTaskManager.IdProcess, 100)
        lvProcess.Columns.Add(My.Resources.EasyTaskManager.ProcessName, 350)
        lvProcess.Columns.Add(My.Resources.EasyTaskManager.Username, 100)
        lvProcess.Columns.Add(My.Resources.EasyTaskManager.Memory, 100)
        lvProcess.Columns.Add(My.Resources.EasyTaskManager.Status, 100)

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
        For Each p As Process In listProcess
            'lvi = New ListViewItem()
            'lvi.Name = p.Id
            'lvi.Text = p.Id
            'lvi.SubItems.Add(p.ProcessName)
            'lvi.SubItems.Add(p.StartInfo.UserName)
            'lvi.SubItems.Add(p.VirtualMemorySize.ToString)
            'lvi.SubItems.Add("Running")

            'lvProcess.Items.Add(lvi)

            ListViewAddItemProcess(p.Id, p.ProcessName, p.StartInfo.UserName, (p.PrivateMemorySize).ToString(), "Running")
        Next

        NumberProcess(listProcess.Count)
        NumberProcessStatus(listProcess.Count)
        UtilizationCPU(String.Format("{0} %", UsedCPU.ToString("#,###")))
        UtilizationCPUStatus(String.Format("{0} %", UsedCPU.ToString("#,###")))

        _threadProcess.Abort()
        While _threadProcess.IsAlive

        End While

    End Sub

    Private Sub LoadServices()
        Dim listServices As List(Of ServiceController) = ServiceController.GetServices.ToList
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

    Private Sub ListViewAddItemProcess(pId As String, pName As String, pUsername As String, pMemory As String, pStatus As String)
        If lvProcess.InvokeRequired Then
            Dim d As New del_Process(AddressOf ListViewAddItemProcess)
            Me.lvProcess.BeginInvoke(d, {pId, pName, pUsername, pMemory, pStatus})
        Else
            lvProcess.Items.Add(New ListViewItem(New String() {pId, pName, pUsername, pMemory, pStatus}))
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
                Dim cmsChangePriorityProcess As ToolStripItem = cms.Items.Add(My.Resources.EasyTaskManager.ChangePriorityProcess)
                Dim cmsFinalizeTask As ToolStripItem = cms.Items.Add(My.Resources.EasyTaskManager.FinalizeTask)
                Dim cmsFinalizeTreeProcess As ToolStripItem = cms.Items.Add(My.Resources.EasyTaskManager.FinalizeTreeProcess)
                Dim cmsGotoServices As ToolStripItem = cms.Items.Add(My.Resources.EasyTaskManager.GotoServices)

                ' Dim tsiPriorityNormal As ToolStripItem = cms.Items.AddRange(My.Resources.EasyTaskManager.PriorityNormal)

                'tsiPriorityNormal.Owne = cmsChangePriorityProcess

                AddHandler cmsFinalizeTask.Click, AddressOf FinalizeTask_Click
                AddHandler cmsFinalizeTreeProcess.Click, AddressOf FinalizeTreeProcess_Click
                AddHandler cmsGotoServices.Click, AddressOf GotoServices_Click
        End Select
        Return cms
    End Function

    Private Sub GotoServices_Click(sender As Object, e As EventArgs)
        tpServices.Select()
    End Sub

    Private Sub FinalizeTreeProcess_Click(sender As Object, e As EventArgs)
        If lvProcess.SelectedItems.Count > 0 Then
            Dim p As Process = Process.GetProcessById(lvProcess.SelectedItems(0).Text)
            p.Kill()
            p.WaitForExit()
        End If
    End Sub

    Private Sub ChangePriorityProcess_Click(sender As Object, e As EventArgs)
        If lvProcess.SelectedItems.Count > 0 Then
            Dim p As Process = Process.GetProcessById(lvProcess.SelectedItems(0).Text)
            p.PriorityClass = ProcessPriorityClass.Normal
            p.WaitForExit()
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
                lvProcess.ContextMenuStrip = ConfigureContextMenuEasyTaskManager(typeTask)
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

End Class

Public Enum eType
    GeneralInformation = 1
    Processes = 2
    Services = 3
End Enum
