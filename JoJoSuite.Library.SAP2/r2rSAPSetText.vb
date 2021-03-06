Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Public Class r2rSAPSetText

    Private _controlId As String

    Private _value As String

    Private _sapGuiSession As Object

    Private ControlType As String

    Private _error As Boolean = True

    Private _errorMsg As String = "DoAction() method not called"

    Public Property ControlID As String
        Get
            Return _controlId
        End Get

        Set(ByVal value As String)
            _controlId = value
        End Set
    End Property

    Public Property Value As String
        Get
            Return _value
        End Get

        Set(ByVal value As String)
            _value = value
        End Set
    End Property

    Public Property SAPGuiSession As Object
        Get
            Return _sapGuiSession
        End Get

        Set(ByVal value As Object)
            _sapGuiSession = value
        End Set
    End Property

    Public ReadOnly Property [Error] As Boolean
        Get
            Return _error
        End Get
    End Property

    Public ReadOnly Property ErrorMessage As String
        Get
            Return _errorMsg
        End Get
    End Property

    Public Function DoAction() As Boolean
        Dim res As Boolean = False
        Try
            ''Threading.Thread.Sleep(2000)
            _sapGuiSession.FindById(ControlID).setfocus()
            _sapGuiSession.FindById(ControlID).Text = Value

            _error = False
            _errorMsg = ""
            res = True
        Catch ex As Exception
            res = False
            _error = True
            _errorMsg = Me.[GetType]().ToString() & ":" & vbLf + ex.Message
        End Try

        Return res
    End Function
End Class
