Public Class frmAfinidad

    Private _process As Process = Nothing

    Public Sub New(pProcess As Process)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        _process = pProcess
        lblTitle.Text = String.Format("¿Qué procesadores pueden ejecutar {0}?", pProcess.ProcessName)
        LoadProcessor()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Private Sub LoadProcessor()
        Dim coreCount As Integer = Environment.ProcessorCount
        ListView1.View = View.Details
        ListView1.Columns.Add("CPU")

        ListView1.Items.Clear()

        For i As Int16 = 0 To coreCount - 1
            ListView1.Items.Add(String.Format("CPU [ {0} ]", i))
        Next
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            Dim cpu As Integer = ListView1.SelectedIndices(0)
            System.Threading.Thread.BeginThreadAffinity()
            _process.ProcessorAffinity = (2 ^ cpu) - 1
            System.Threading.Thread.EndThreadAffinity()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub
End Class