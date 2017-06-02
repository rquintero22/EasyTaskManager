Public Class frmNewProcess

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        Initialize
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Private Sub Initialize()
        LoadedResources()

        AddHandler btnOk.Click, AddressOf btnOk_Click
        AddHandler btnCancel.Click, AddressOf btnCancel_Click

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs)
        If txtTask.Text.ToString.Length > 0 Then
            Process.Start(txtTask.Text.ToString)
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Loaded resources the system
    ''' </summary>
    Private Sub LoadedResources()
        Me.Text = My.Resources.EasyTaskManager.CreateTask

        lblHead.Text = My.Resources.EasyTaskManager.Head
        lblOpen.Text = My.Resources.EasyTaskManager.OpenProcess
        btnOk.Text = My.Resources.EasyTaskManager.Ok
        btnCancel.Text = My.Resources.EasyTaskManager.Cancel

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim ofd As New OpenFileDialog
        If ofd.ShowDialog = DialogResult.OK Then
            txtTask.Text = ofd.FileName
        End If

    End Sub

    Private Sub frmNewProcess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.txtTask.Clear()
        Me.txtTask.Focus()
    End Sub

End Class