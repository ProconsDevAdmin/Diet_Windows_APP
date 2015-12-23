Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class SQLDataAccess

    Private sqlAdap As SqlDataAdapter
    Private Ds As DataSet
    Private sqlCmd As SqlCommand
    Private err As String
    Public RF As Boolean

    Private Function getConnectionString(ByVal strCompany As String)
        Dim _retVal As String = String.Empty
        Try
            Dim strConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("SqlConnection")
            _retVal = String.Format(strConnectionString, strCompany)
        Catch ex As Exception
            Throw ex
        End Try
        Return _retVal
    End Function

    Public Function ExecuteDataTable(ByVal strCompany As String, ByVal strQuery As String) As DataTable
        Dim _retVal As DataTable = Nothing
        Dim ConnectionString As String = getConnectionString(strCompany)
        Dim myConnection As SqlConnection = New SqlConnection(ConnectionString)
        Ds = New DataSet
        Try
            myConnection.Open()
            sqlAdap = New SqlDataAdapter(strQuery, myConnection)
            sqlAdap.Fill(Ds, "T_Temp")
            _retVal = Ds.Tables("T_Temp")
        Catch ex As Exception
            err = ex.ToString
            myConnection.Close()
        Finally
            myConnection.Close()
        End Try
        Return _retVal
    End Function

    Public Sub ExecuteNonQuery(ByVal strCompany As String, ByVal strQuery As String)
        Dim ConnectionString As String = getConnectionString(strCompany)
        Dim myConnection As SqlConnection = New SqlConnection(ConnectionString)
        Try
            myConnection.Open()
            sqlCmd = New SqlCommand(strQuery, myConnection)
            sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            err = ex.ToString
            myConnection.Close()
        Finally
            myConnection.Close()
        End Try
    End Sub

    Public Function ExecuteScalar(ByVal strCompany As String, ByVal strQuery As String) As Integer
        Dim _retVal As Integer
        Dim ConnectionString As String = getConnectionString(strCompany)
        Dim myConnection As SqlConnection = New SqlConnection(ConnectionString)
        Try
            myConnection.Open()
            sqlCmd = New SqlCommand(strQuery, myConnection)
            _retVal = sqlCmd.ExecuteScalar()
        Catch ex As Exception
            err = ex.ToString
            myConnection.Close()
        Finally
            myConnection.Close()
        End Try
        Return _retVal
    End Function

    Public Function ExecuteReader(ByVal strCompany As String, ByVal strQuery As String) As Object
        Dim _retVal As Object = Nothing
        Dim ConnectionString As String = getConnectionString(strCompany)
        Dim myConnection As New SqlConnection(ConnectionString)
        Try
            myConnection.Open()
            sqlCmd = New SqlCommand(strQuery, myConnection)
            Dim myReader As SqlDataReader = sqlCmd.ExecuteReader()
            If myReader.HasRows Then
                _retVal = myReader
            Else
                _retVal = myReader
            End If
            Return _retVal
        Catch ex As Exception
            Throw ex
        Finally

        End Try
        Return _retVal
    End Function

    Public Function ExecuteReader(ConnectionString As String, strQuery As String, strColumn As String) As Object
        Dim _retVal As Object = Nothing
        Dim myConnection As New SqlConnection(ConnectionString)
        Try
            myConnection.Open()
            sqlCmd = New SqlCommand(strQuery, myConnection)
            Dim myReader As SqlDataReader = sqlCmd.ExecuteReader()
            If myReader.HasRows Then
                _retVal = myReader(strColumn)
            Else
                _retVal = ""
            End If
            Return _retVal
        Catch ex As Exception
            Throw ex
        Finally
            'myConnection.Close()
            'myConnection = Nothing
        End Try
        Return _retVal
    End Function

End Class

Public Module Singleton

    Private oSQLDataAccess As SQLDataAccess
    Private objCompany As SAPbobsCOM.Company

    Public ReadOnly Property GetSQLDataObject() As SQLDataAccess
        Get
            If Not IsNothing(oSQLDataAccess) Then
                Return oSQLDataAccess
            Else
                oSQLDataAccess = New SQLDataAccess()
                Return oSQLDataAccess
            End If
        End Get
    End Property

    Public ReadOnly Property getSAPCompany() As SAPbobsCOM.Company
        Get
            Return objCompany
        End Get
    End Property

    'Public Property ConnectSapCompany(ByVal strCompanyDB As String, ByVal strSAPUserName As String, ByVal strSAPPwd As String) As SAPbobsCOM.Company
    '    Get
    '        If IsNothing(objCompany) Then
    '            objCompany = connectCompany(strCompanyDB, strSAPUserName, strSAPPwd)
    '        End If
    '        Return objCompany
    '    End Get
    '    Set(value As SAPbobsCOM.Company)
    '        objCompany = value
    '    End Set
    'End Property

    Public Function ConnectSAPCompany(ByVal strCompanyDB As String, ByVal strSAPUserName As String, ByVal strSAPPwd As String) As SAPbobsCOM.Company
        Dim _retVal As SAPbobsCOM.Company
        Try

            Dim LicenseServer As String = System.Configuration.ConfigurationManager.AppSettings("LicenseServer").ToString()
            Dim DBServer As String = System.Configuration.ConfigurationManager.AppSettings("DBServer").ToString()
            Dim ServerType As String = System.Configuration.ConfigurationManager.AppSettings("ServerType").ToString()
            Dim DBUserName As String = System.Configuration.ConfigurationManager.AppSettings("SqlUser").ToString()
            Dim DBPwd As String = System.Configuration.ConfigurationManager.AppSettings("SqlPwd").ToString()

            'Dim CompanyDB As String = System.Configuration.ConfigurationManager.AppSettings("SAPCompany").ToString()
            'Dim SBOUserid As String = "manager"
            'Dim SBOPwd As String = "1234"

            _retVal = New SAPbobsCOM.Company()

            If (ServerType = "2005") Then
                _retVal.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005
            ElseIf (ServerType = "2008") Then
                _retVal.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008
            End If

            _retVal.LicenseServer = LicenseServer
            _retVal.Server = DBServer
            _retVal.UseTrusted = False
            _retVal.DbUserName = DBUserName
            _retVal.DbPassword = DBPwd
            _retVal.CompanyDB = strCompanyDB
            _retVal.UserName = strSAPUserName
            _retVal.Password = strSAPPwd

            If (_retVal.Connect() <> 0) Then
                Throw New Exception(_retVal.GetLastErrorDescription())
            End If
            objCompany = _retVal
        Catch ex As Exception
            Throw ex
        End Try
        Return _retVal
    End Function

    Public Sub DisConnectSAPCompany()
        Try
            If IsNothing(objCompany) Then
                If objCompany.Connected Then
                    objCompany.Disconnect()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Module
