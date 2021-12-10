Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Public Class r2rSAPScrollBar

    Private _controlId As String

    Private _position As String

    Private _sapGuiSession As Object


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

    Public Property Position As String
        Get
            Return _position
        End Get

        Set(ByVal value As String)
            _position = value
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
            _sapGuiSession.FindById(ControlID).verticalScrollbar.position = _position

            _error = False
            _errorMsg = ""
            res = True
        Catch ex As Exception
            res = False
            _error = True
            _errorMsg = [GetType]().ToString() & ":" & vbLf + ex.Message
        End Try

        Return res
    End Function
End Class
