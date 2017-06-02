<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.tcEasyTaskManager = New System.Windows.Forms.TabControl()
        Me.tpGeneralInformation = New System.Windows.Forms.TabPage()
        Me.lblSubProcess = New System.Windows.Forms.Label()
        Me.lblTitleSubProcess = New System.Windows.Forms.Label()
        Me.lblProcess = New System.Windows.Forms.Label()
        Me.lblTitleProcess = New System.Windows.Forms.Label()
        Me.lblMemory = New System.Windows.Forms.Label()
        Me.lblTitleMemory = New System.Windows.Forms.Label()
        Me.lblCPU = New System.Windows.Forms.Label()
        Me.lblTitleCPU = New System.Windows.Forms.Label()
        Me.tpProcesses = New System.Windows.Forms.TabPage()
        Me.lvProcess = New System.Windows.Forms.ListView()
        Me.tpServices = New System.Windows.Forms.TabPage()
        Me.lvServices = New System.Windows.Forms.ListView()
        Me.mnusEasyTaskManager = New System.Windows.Forms.MenuStrip()
        Me.tsmnuiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmnuiNewTask = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmnuiClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.ssEasyTaskManager = New System.Windows.Forms.StatusStrip()
        Me.cmsEasyTaskManager = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.tcEasyTaskManager.SuspendLayout()
        Me.tpGeneralInformation.SuspendLayout()
        Me.tpProcesses.SuspendLayout()
        Me.tpServices.SuspendLayout()
        Me.mnusEasyTaskManager.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.tcEasyTaskManager, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.mnusEasyTaskManager, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ssEasyTaskManager, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 83.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.90173!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.09827!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(992, 434)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'tcEasyTaskManager
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tcEasyTaskManager, 2)
        Me.tcEasyTaskManager.Controls.Add(Me.tpGeneralInformation)
        Me.tcEasyTaskManager.Controls.Add(Me.tpProcesses)
        Me.tcEasyTaskManager.Controls.Add(Me.tpServices)
        Me.tcEasyTaskManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcEasyTaskManager.Location = New System.Drawing.Point(3, 111)
        Me.tcEasyTaskManager.Name = "tcEasyTaskManager"
        Me.tcEasyTaskManager.SelectedIndex = 0
        Me.tcEasyTaskManager.Size = New System.Drawing.Size(986, 251)
        Me.tcEasyTaskManager.TabIndex = 1
        '
        'tpGeneralInformation
        '
        Me.tpGeneralInformation.Controls.Add(Me.lblSubProcess)
        Me.tpGeneralInformation.Controls.Add(Me.lblTitleSubProcess)
        Me.tpGeneralInformation.Controls.Add(Me.lblProcess)
        Me.tpGeneralInformation.Controls.Add(Me.lblTitleProcess)
        Me.tpGeneralInformation.Controls.Add(Me.lblMemory)
        Me.tpGeneralInformation.Controls.Add(Me.lblTitleMemory)
        Me.tpGeneralInformation.Controls.Add(Me.lblCPU)
        Me.tpGeneralInformation.Controls.Add(Me.lblTitleCPU)
        Me.tpGeneralInformation.Location = New System.Drawing.Point(4, 22)
        Me.tpGeneralInformation.Name = "tpGeneralInformation"
        Me.tpGeneralInformation.Padding = New System.Windows.Forms.Padding(3)
        Me.tpGeneralInformation.Size = New System.Drawing.Size(978, 225)
        Me.tpGeneralInformation.TabIndex = 0
        Me.tpGeneralInformation.Text = "General Information"
        Me.tpGeneralInformation.UseVisualStyleBackColor = True
        '
        'lblSubProcess
        '
        Me.lblSubProcess.AutoSize = True
        Me.lblSubProcess.Location = New System.Drawing.Point(365, 59)
        Me.lblSubProcess.Name = "lblSubProcess"
        Me.lblSubProcess.Size = New System.Drawing.Size(45, 13)
        Me.lblSubProcess.TabIndex = 1
        Me.lblSubProcess.Text = "Process"
        '
        'lblTitleSubProcess
        '
        Me.lblTitleSubProcess.AutoSize = True
        Me.lblTitleSubProcess.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleSubProcess.Location = New System.Drawing.Point(360, 30)
        Me.lblTitleSubProcess.Name = "lblTitleSubProcess"
        Me.lblTitleSubProcess.Size = New System.Drawing.Size(110, 20)
        Me.lblTitleSubProcess.TabIndex = 1
        Me.lblTitleSubProcess.Tag = " "
        Me.lblTitleSubProcess.Text = "Sub Process"
        '
        'lblProcess
        '
        Me.lblProcess.AutoSize = True
        Me.lblProcess.Location = New System.Drawing.Point(261, 59)
        Me.lblProcess.Name = "lblProcess"
        Me.lblProcess.Size = New System.Drawing.Size(45, 13)
        Me.lblProcess.TabIndex = 1
        Me.lblProcess.Text = "Process"
        '
        'lblTitleProcess
        '
        Me.lblTitleProcess.AutoSize = True
        Me.lblTitleProcess.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleProcess.Location = New System.Drawing.Point(256, 30)
        Me.lblTitleProcess.Name = "lblTitleProcess"
        Me.lblTitleProcess.Size = New System.Drawing.Size(73, 20)
        Me.lblTitleProcess.TabIndex = 1
        Me.lblTitleProcess.Text = "Process"
        '
        'lblMemory
        '
        Me.lblMemory.AutoSize = True
        Me.lblMemory.Location = New System.Drawing.Point(67, 113)
        Me.lblMemory.Name = "lblMemory"
        Me.lblMemory.Size = New System.Drawing.Size(44, 13)
        Me.lblMemory.TabIndex = 1
        Me.lblMemory.Text = "Memory"
        '
        'lblTitleMemory
        '
        Me.lblTitleMemory.AutoSize = True
        Me.lblTitleMemory.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleMemory.Location = New System.Drawing.Point(62, 84)
        Me.lblTitleMemory.Name = "lblTitleMemory"
        Me.lblTitleMemory.Size = New System.Drawing.Size(71, 20)
        Me.lblTitleMemory.TabIndex = 1
        Me.lblTitleMemory.Text = "Memory"
        '
        'lblCPU
        '
        Me.lblCPU.AutoSize = True
        Me.lblCPU.Location = New System.Drawing.Point(71, 59)
        Me.lblCPU.Name = "lblCPU"
        Me.lblCPU.Size = New System.Drawing.Size(29, 13)
        Me.lblCPU.TabIndex = 1
        Me.lblCPU.Text = "CPU"
        '
        'lblTitleCPU
        '
        Me.lblTitleCPU.AutoSize = True
        Me.lblTitleCPU.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleCPU.Location = New System.Drawing.Point(66, 30)
        Me.lblTitleCPU.Name = "lblTitleCPU"
        Me.lblTitleCPU.Size = New System.Drawing.Size(45, 20)
        Me.lblTitleCPU.TabIndex = 1
        Me.lblTitleCPU.Text = "CPU"
        '
        'tpProcesses
        '
        Me.tpProcesses.Controls.Add(Me.lvProcess)
        Me.tpProcesses.Location = New System.Drawing.Point(4, 22)
        Me.tpProcesses.Name = "tpProcesses"
        Me.tpProcesses.Padding = New System.Windows.Forms.Padding(3)
        Me.tpProcesses.Size = New System.Drawing.Size(978, 225)
        Me.tpProcesses.TabIndex = 1
        Me.tpProcesses.Text = "Process"
        Me.tpProcesses.UseVisualStyleBackColor = True
        '
        'lvProcess
        '
        Me.lvProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcess.Location = New System.Drawing.Point(3, 3)
        Me.lvProcess.Name = "lvProcess"
        Me.lvProcess.Size = New System.Drawing.Size(972, 219)
        Me.lvProcess.TabIndex = 1
        Me.lvProcess.UseCompatibleStateImageBehavior = False
        '
        'tpServices
        '
        Me.tpServices.Controls.Add(Me.lvServices)
        Me.tpServices.Location = New System.Drawing.Point(4, 22)
        Me.tpServices.Name = "tpServices"
        Me.tpServices.Size = New System.Drawing.Size(978, 225)
        Me.tpServices.TabIndex = 2
        Me.tpServices.Text = "Services"
        Me.tpServices.UseVisualStyleBackColor = True
        '
        'lvServices
        '
        Me.lvServices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvServices.Location = New System.Drawing.Point(0, 0)
        Me.lvServices.Name = "lvServices"
        Me.lvServices.Size = New System.Drawing.Size(978, 225)
        Me.lvServices.TabIndex = 1
        Me.lvServices.UseCompatibleStateImageBehavior = False
        '
        'mnusEasyTaskManager
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.mnusEasyTaskManager, 2)
        Me.mnusEasyTaskManager.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmnuiFile})
        Me.mnusEasyTaskManager.Location = New System.Drawing.Point(0, 83)
        Me.mnusEasyTaskManager.Name = "mnusEasyTaskManager"
        Me.mnusEasyTaskManager.Size = New System.Drawing.Size(992, 24)
        Me.mnusEasyTaskManager.TabIndex = 3
        Me.mnusEasyTaskManager.Text = "MenuStrip1"
        '
        'tsmnuiFile
        '
        Me.tsmnuiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmnuiNewTask, Me.tsmnuiClose})
        Me.tsmnuiFile.Name = "tsmnuiFile"
        Me.tsmnuiFile.Size = New System.Drawing.Size(37, 20)
        Me.tsmnuiFile.Text = "File"
        '
        'tsmnuiNewTask
        '
        Me.tsmnuiNewTask.Name = "tsmnuiNewTask"
        Me.tsmnuiNewTask.Size = New System.Drawing.Size(152, 22)
        Me.tsmnuiNewTask.Text = "New Task"
        '
        'tsmnuiClose
        '
        Me.tsmnuiClose.Name = "tsmnuiClose"
        Me.tsmnuiClose.Size = New System.Drawing.Size(152, 22)
        Me.tsmnuiClose.Text = "Close"
        '
        'ssEasyTaskManager
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.ssEasyTaskManager, 2)
        Me.ssEasyTaskManager.Location = New System.Drawing.Point(0, 412)
        Me.ssEasyTaskManager.Name = "ssEasyTaskManager"
        Me.ssEasyTaskManager.Size = New System.Drawing.Size(992, 22)
        Me.ssEasyTaskManager.SizingGrip = False
        Me.ssEasyTaskManager.TabIndex = 2
        Me.ssEasyTaskManager.Text = "StatusStrip1"
        '
        'cmsEasyTaskManager
        '
        Me.cmsEasyTaskManager.Name = "cmsEasyTaskManager"
        Me.cmsEasyTaskManager.Size = New System.Drawing.Size(61, 4)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 434)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnusEasyTaskManager
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Easy Task Manager"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.tcEasyTaskManager.ResumeLayout(False)
        Me.tpGeneralInformation.ResumeLayout(False)
        Me.tpGeneralInformation.PerformLayout()
        Me.tpProcesses.ResumeLayout(False)
        Me.tpServices.ResumeLayout(False)
        Me.mnusEasyTaskManager.ResumeLayout(False)
        Me.mnusEasyTaskManager.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents tcEasyTaskManager As TabControl
    Friend WithEvents tpGeneralInformation As TabPage
    Friend WithEvents tpProcesses As TabPage
    Friend WithEvents lvProcess As ListView
    Friend WithEvents tpServices As TabPage
    Friend WithEvents lvServices As ListView
    Friend WithEvents lblMemory As Label
    Friend WithEvents lblTitleMemory As Label
    Friend WithEvents lblCPU As Label
    Friend WithEvents lblTitleCPU As Label
    Friend WithEvents lblProcess As Label
    Friend WithEvents lblTitleProcess As Label
    Friend WithEvents lblSubProcess As Label
    Friend WithEvents lblTitleSubProcess As Label
    Friend WithEvents ssEasyTaskManager As StatusStrip
    Friend WithEvents mnusEasyTaskManager As MenuStrip
    Friend WithEvents tsmnuiFile As ToolStripMenuItem
    Friend WithEvents tsmnuiNewTask As ToolStripMenuItem
    Friend WithEvents tsmnuiClose As ToolStripMenuItem
    Friend WithEvents cmsEasyTaskManager As ContextMenuStrip
End Class
