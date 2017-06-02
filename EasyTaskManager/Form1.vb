Imports System.ServiceProcess
Imports System.Threading

Public Class Form1

#Region "Variables"
    Private _rm As Resources.ResourceManager
    Private _threadProcess As Thread = Nothing
    Private _threadServices As Thread = Nothing

    Private _tmrProcess As System.Windows.Forms.Timer = Nothing
    Private _tmrServices As System.Windows.Forms.Timer = Nothing

    Private Delegate Sub del_Process()
    Private Delegate Sub del_Services()

#End Region

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        Initialize()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Private Sub Initialize()
        LoadedResources

        ConfigureListView()
        ConfigureListViewServices()

        'Customized timers
        'Process
        _tmrProcess = New System.Windows.Forms.Timer
        _tmrProcess.Interval = 15000

        'Services 
        _tmrServices = New System.Windows.Forms.Timer
        _tmrServices.Interval = 15000

        'Handler timer
        AddHandler _tmrProcess.Tick, AddressOf TmrProcess_Tick
        AddHandler _tmrServices.Tick, AddressOf TmrServices_Tick


        'Load process
        _threadProcess = New Thread(New ThreadStart(AddressOf LoadServices))
        ''Load services
        _threadServices = New Thread(New ThreadStart(AddressOf LoadServices))

        _tmrProcess.Start()
        _tmrServices.Start()

        'LoadProcess()
    End Sub

    Private Sub TmrServices_Tick(sender As Object, e As EventArgs)
        If Not _threadServices.IsAlive Then
            _threadServices.Start()
        End If
    End Sub

    Private Sub TmrProcess_Tick(sender As Object, e As EventArgs)
        If Not _threadProcess.IsAlive Then
            _threadProcess.Start()
        End If
    End Sub

    Private Sub ConfigureListView()
        lvProcess.Columns.Clear()
        lvProcess.Items.Clear()

        lvProcess.Columns.Add("Id", 100)
        lvProcess.Columns.Add("Name", 350)
        lvProcess.Columns.Add("Username", 100)
        lvProcess.Columns.Add("Memory", 100)
        lvProcess.Columns.Add("Status", 100)

        lvProcess.View = View.Details
    End Sub

    Private Sub ConfigureListViewServices()
        lvServices.Columns.Clear()
        lvServices.Items.Clear()

        lvServices.Columns.Add("Id", 200)
        lvServices.Columns.Add("Name", 350)
        lvServices.Columns.Add("Machine", 100)
        lvServices.Columns.Add("Status", 100)

        lvServices.View = View.Details
    End Sub

    Private Sub LoadProcess()
        Dim listProcess As List(Of Process) = Process.GetProcesses.ToList
        Dim lvi As ListViewItem = Nothing
        Dim statusProcess As Boolean = Nothing
        For Each p As Process In listProcess
            lvi = New ListViewItem()
            lvi.Name = p.Id
            lvi.Text = p.Id
            lvi.SubItems.Add(p.ProcessName)
            lvi.SubItems.Add(p.StartInfo.UserName)
            lvi.SubItems.Add(String.Format("{0} K", p.workingset64 / 1024).ToString("#.##"))
            lvi.SubItems.Add("Running")

            lvProcess.Items.Add(lvi)
        Next

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

            lvServices.Items.Add(lviServices)
        Next

    End Sub

    Private Sub LoadedResources()
        Me.text = MyBase.resources.EasyTaskManager.ApplicationName
        lblCpu.text = MyBase.resources.EasyTaskManager.cpu
        lbl
    End Sub

    Private Sub ConfigureCulture()
        Threading.Thread.CurrentThread.CurrentUICulture = _cultureInfo

        If _cultureInfo.Name.Contains("es-CR") Then
            _rm = New Resources.ResourceManager("SeguridadWF.My.Resources.IntegraSeguridad.es-CR", Assembly.GetExecutingAssembly())
        Else
            _rm = New Resources.ResourceManager("SeguridadWF.My.Resources.IntegraSeguridad", Assembly.GetExecutingAssembly())
        End If
    End Sub

End Class
