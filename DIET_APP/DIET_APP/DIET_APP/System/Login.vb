''*****************************************************************
'Copyright information			: 
'File Name						: Login.vb
'Author							: 
'Date of creation(mm/dd/yyyy)	: 
'Description					: This Class is Used to Login into DIET System...
'Version Number					: 1.0
'Revision History : 
'*****************************************************************
' Date Modified          Modified by	        Brief Description 
'

Imports System.Configuration
Imports System.Configuration.ConfigurationSettings
Imports System.Configuration.ConfigurationManager

Public Class Login

#Region "Declaration"
    Public UserId As String
    Public strSqlUser As String
    Public strSqlPwd As String
    Public UserType As String
    Dim strQry As String = String.Empty
#End Region

#Region "Event"
    Private Sub Login_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            'UXUTIL.clsUtilities.setAllControlsThemes(Me)
            scHeader.BackColor = Color.FromArgb(61, 122, 153)
            pnlFooter.BackColor = Color.FromArgb(255, 255, 255)
            ErrMsg.BackColor = Color.FromArgb(255, 255, 255)
            lblMask.ForeColor = Color.FromArgb(210, 210, 210)
            Label2.Text = "Version : " + Application.ProductVersion + ""
            LoadCombo()
            Me.CenterToScreen()
        Catch ex As Exception
            MsgBoxNew.Shows(ex.Message, "Error", MsgBoxNew.Buttons.OKCancel, MsgBoxNew.Icons.Info)
        End Try
    End Sub

    Private Sub LoadCombo()
        Try
            Dim strDBServers = System.Configuration.ConfigurationManager.AppSettings("DBServer").ToString
            Dim DBserverList As String() = strDBServers.Split(",")
            cmbDBServer.DataSource = DBserverList
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LoadGrid()
        Dim oDr As SqlClient.SqlDataReader = Nothing
        Try
            Dim oDT As New DataTable
            Dim strQry As String = " Select cmpName As 'Company',dbName As 'Database' From SRGC "
            oDr = Singleton.GetSQLDataObject().ExecuteReader("SBO-COMMON", strQry)
            oDT.Load(oDr)
            oDr.Close()
            Rowmatrix.DataSource = oDT
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(oDr) Then
                If Not oDr.IsClosed Then
                    oDr.Close()
                End If
            End If
        End Try
    End Sub

    Private Sub Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add.Click
        Try
            If UserCode.Text.ToString.Length = 0 Then
                ErrMsg.Text = "UserCode Cannot be Empty!!"
                Exit Sub
            ElseIf Password.Text.ToString.Length = 0 Then
                ErrMsg.Text = "Password Cannot be Empty!!"
            Else
                Login()
            End If
        Catch ex As Exception
            MsgBoxNew.Shows(ex.Message, "Error", MsgBoxNew.Buttons.OKCancel, MsgBoxNew.Icons.Info)
        End Try
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        End
    End Sub

    Private Sub cmbDBServer_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDBServer.SelectedValueChanged
        Try
            LoadGrid()
        Catch ex As Exception
            MsgBoxNew.Shows(ex.Message, "Error", MsgBoxNew.Buttons.OKCancel, MsgBoxNew.Icons.Info)
        End Try
    End Sub

#End Region

#Region "Function"
    Private Sub Login()
        Dim oDr As SqlClient.SqlDataReader = Nothing
        Try
            If Rowmatrix.Rows.Count > 0 Then
                Dim oDT As New DataTable
                strQry = " Select USER_CODE From OUSR Where USER_CODE = '" + UserCode.Text.ToString.Trim + "'"
                oDr = Singleton.GetSQLDataObject().ExecuteReader(Rowmatrix.SelectedRows(0).Cells("Database").Value.ToString, strQry)
                UserId = UserCode.Text.ToString
                If Not oDr.HasRows Then
                    ErrMsg.Text = "Invalid Combination of UserName and Password"
                    oDr.Close()
                    Exit Sub
                ElseIf oDr.HasRows Then
                    oDT.Load(oDr)
                    oDr.Close()
                    Singleton.ConnectSAPCompany(Rowmatrix.SelectedRows(0).Cells("Database").Value.ToString, UserCode.Text, Password.Text)
                    If Singleton.getSAPCompany.Connected Then
                        MainForm.Show()
                        Me.Hide()
                    Else
                        ErrMsg.Text = Singleton.getSAPCompany.GetLastErrorDescription()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBoxNew.Shows(ex.Message, "Error", MsgBoxNew.Buttons.OKCancel, MsgBoxNew.Icons.Info)
        Finally
            If Not IsNothing(oDr) Then
                If Not oDr.IsClosed Then
                    oDr.Close()
                End If
            End If
        End Try
    End Sub

#End Region
   
End Class