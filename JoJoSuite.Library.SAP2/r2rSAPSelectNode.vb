Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Public Class r2rSAPSelectNode
    Private _controlId As String

    Private _nodekey As String
    Private _nodetext As String
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

    Public Property NodeKey As String
        Get
            Return _nodekey
        End Get

        Set(ByVal NodeKey As String)
            _nodekey = NodeKey
        End Set
    End Property
    Public Property NodeText As String
        Get
            Return _nodetext
        End Get

        Set(ByVal NodeText As String)
            _nodetext = NodeText
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
            Dim nodeObj = _sapGuiSession.FindById(_controlId)
            If _nodekey <> "" Then
                Threading.Thread.Sleep(2000)
                nodeObj.selectedNode(_nodekey)
                nodeObj.DoubleClickNode(_nodekey)

            Else
                Threading.Thread.Sleep(2000)
                For Each key As String In nodeObj.GetAllNodeKeys()
                    If nodeObj.GetNodeTextByKey(key).Contains(_nodetext) Then
                        nodeObj.selectedNode(key)
                        nodeObj.DoubleClickNode(key)
                    End If
                Next
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
