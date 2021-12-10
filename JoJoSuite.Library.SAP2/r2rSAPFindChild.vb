Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Public Class r2rSAPFindChild
    Private _controlId As String
    Private _sapGuiSession As Object

    Private _headerText As String
    Private _ChildCount As Int32
    Private _isAvailable As Boolean = False
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



    Public Property SAPGuiSession As Object
        Get
            Return _sapGuiSession
        End Get

        Set(ByVal value As Object)
            _sapGuiSession = value
        End Set
    End Property
    Public ReadOnly Property HeaderText As String
        Get
            Return _headerText
        End Get
    End Property
    Public ReadOnly Property ChildCount As Int32
        Get
            Return _ChildCount
        End Get
    End Property
    Public ReadOnly Property isAvailable As Boolean
        Get
            Return _isAvailable
        End Get
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
            'if _controlId is Empty
            If _controlId Is Nothing Then
                'Get window count
                _ChildCount = _sapGuiSession.Children.Count
            Else
                Dim ControlObj = _sapGuiSession.FindById(_controlId)
                If ControlObj IsNot Nothing Then
                    'To check the control is available or not
                    _isAvailable = True
                    'To read header text 
                    _headerText = ControlObj.Text
                End If
                _ChildCount = _sapGuiSession.Children.Count
            End If

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
