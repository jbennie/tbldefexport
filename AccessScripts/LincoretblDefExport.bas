Attribute VB_Name = "LincoretblDefExport"
Option Compare Database

 
Sub ExportStructs()

Call FieldNames("Affiliates")
Call FieldNames("Agents")
Call FieldNames("Site")
Call FieldNames("Sites")
Call FieldNames("Site Owners")
'Call FieldNames("Contacts")
Call FieldNames("Contacts - Organisations")
Call FieldNames("Contacts from Outlook")
Call FieldNames("Contacts List - Individuals")
Call FieldNames("Contacts List - Main")
Call FieldNames("Contract Details")
Call FieldNames("Council Details")

Call FieldNames("Land Agents")

Call FieldNames("Offer Details")
Call FieldNames("Order Types")
Call FieldNames("Originator Company")
Call FieldNames("Originator Individual")
Call FieldNames("Owners")
Call FieldNames("PApps Agent")
'Call FieldNames("PApps Agent Address / Tel")
Call FieldNames("PApps DC")
Call FieldNames("PApps Town")
Call FieldNames("PApps Type")
Call FieldNames("Planning Application Types")
Call FieldNames("Planning Status")
Call FieldNames("Purchaser Details")

Call FieldNames("GreensheetData")
Call FieldNames("Greensheet Details")
Call FieldNames("Greensheet Additional Details")

Call FieldNames("Developer County Requirements")
Call FieldNames("Developers' Requirements Main")

MsgBox "done"

End Sub


Sub FieldNames(tbl As String)
    Dim Rst As Recordset
    Dim f As Field

    Dim aryFields()
    Dim lngCount As Long
    lngCount = 0
    
    Set Rst = CurrentDb.OpenRecordset(tbl)
    
    For Each f In Rst.Fields
        lngCount = lngCount + 1
        ReDim Preserve aryFields(2, lngCount)
       'MsgBox (f.Properties("Type"))GetFieldDesc_ADO(tbl, f.Name)
    
    If lngCount = 1 Then
        aryFields(1, lngCount) = "TBL_Fields.Add(new VTable_FieldDef(""[" & (f.Name) & "]"", ""System." & DotNetFieldTypeName(f) & """, """ & Replace(Replace(Replace(LCase(f.Name), " ", ""), "/", "_"), "-", "") & """, true, " & lngCount & " )); "
        Else
        aryFields(1, lngCount) = "TBL_Fields.Add(new VTable_FieldDef(""[" & (f.Name) & "]"", ""System." & DotNetFieldTypeName(f) & """, """ & Replace(Replace(Replace(LCase(f.Name), " ", ""), "/", "_"), "-", "") & """, false, " & lngCount & ")); "
    End If
        
    Next
    Rst.Close
    
    If lngCount > 0 Then
        aFileNum = FreeFile
        Open "C:\RWH\tbl_" & tbl & "_struct.cs" For Output As #aFileNum
        
        Print #aFileNum, "[" & tbl & "]" & vbNewLine
        For i = 1 To UBound(aryFields(), 2)
        Print #aFileNum, aryFields(1, i) & vbTab
        Next
        Close #aFileNum
    End If
        
    
End Sub




Function GetFieldDesc_ADO(ByVal MyTableName As String, ByVal MyFieldName As String)
    Dim MyDB As New ADOX.Catalog
    Dim MyTable As ADOX.Table
    Dim MyField As ADOX.Column
    On Error GoTo Err_GetFieldDescription
    MyDB.ActiveConnection = CurrentProject.Connection
    Set MyTable = MyDB.Tables(MyTableName)
    GetFieldDesc_ADO = MyTable.Columns(MyFieldName).Type
    
    '(1).Type
    
    Set MyDB = Nothing
Bye_GetFieldDescription:
    Exit Function
Err_GetFieldDescription:
    Beep
    MsgBox Err.Description, vbExclamation
    GetFieldDescription = Null
    Resume Bye_GetFieldDescription
End Function


Function DotNetFieldTypeName(fld As DAO.Field) As String
    'Purpose: Converts the numeric results of DAO Field.Type to text.
    Dim strReturn As String    'Name to return

    Select Case CLng(fld.Type) 'fld.Type is Integer, but constants are Long.
        Case dbBoolean: strReturn = "Boolean"            ' 1
        Case dbByte: strReturn = "Int32"                 ' 2
        Case dbInteger: strReturn = "Int32"           ' 3
        Case dbLong                                     ' 4
            If (fld.Attributes And dbAutoIncrField) = 0& Then
                strReturn = "Int32"
            Else
                strReturn = "Int32"
            End If
        Case dbCurrency: strReturn = "Double"         ' 5
        Case dbSingle: strReturn = "Int32"             ' 6
        Case dbDouble: strReturn = "Double"             ' 7
        Case dbDate: strReturn = "DateTime"            ' 8
        Case dbBinary: strReturn = "Byte[]"             ' 9 (no interface)
        Case dbText                                     '10
            If (fld.Attributes And dbFixedField) = 0& Then
                strReturn = "String"
            Else
                strReturn = "String"        '(no interface)
            End If
        Case dbLongBinary: strReturn = "OLE Object"     '11
        Case dbMemo                                     '12
            If (fld.Attributes And dbHyperlinkField) = 0& Then
                strReturn = "String"
            Else
                strReturn = "String"
            End If
        Case dbGUID: strReturn = "GUID"                 '15

        'Attached tables only: cannot create these in JET.
        Case dbBigInt: strReturn = "Int64"        '16
        Case dbVarBinary: strReturn = "Byte[]"       '17
        Case dbChar: strReturn = "Char"                 '18
        Case dbNumeric: strReturn = "Double"           '19
        Case dbDecimal: strReturn = "Double"           '20
        Case dbFloat: strReturn = "Float"               '21
        Case dbTime: strReturn = "DateTime"                 '22
        Case dbTimeStamp: strReturn = "Timestamp"      '23

        'Constants for complex types don't work prior to Access 2007 and later.
        Case 101&: strReturn = "Attachment"         'dbAttachment
        Case 102&: strReturn = "Complex Byte"       'dbComplexByte
        Case 103&: strReturn = "Complex Integer"    'dbComplexInteger
        Case 104&: strReturn = "Complex Long"       'dbComplexLong
        Case 105&: strReturn = "Complex Single"     'dbComplexSingle
        Case 106&: strReturn = "Complex Double"     'dbComplexDouble
        Case 107&: strReturn = "Complex GUID"       'dbComplexGUID
        Case 108&: strReturn = "Complex Decimal"    'dbComplexDecimal
        Case 109&: strReturn = "Complex Text"       'dbComplexText
        Case Else: strReturn = "Field type " & fld.Type & " unknown"
    End Select

    DotNetFieldTypeName = strReturn
End Function


Function FieldTypeName(fld As DAO.Field) As String
    'Purpose: Converts the numeric results of DAO Field.Type to text.
    Dim strReturn As String    'Name to return

    Select Case CLng(fld.Type) 'fld.Type is Integer, but constants are Long.
        Case dbBoolean: strReturn = "Yes/No"            ' 1
        Case dbByte: strReturn = "Byte"                 ' 2
        Case dbInteger: strReturn = "Integer"           ' 3
        Case dbLong                                     ' 4
            If (fld.Attributes And dbAutoIncrField) = 0& Then
                strReturn = "Long Integer"
            Else
                strReturn = "AutoNumber"
            End If
        Case dbCurrency: strReturn = "Currency"         ' 5
        Case dbSingle: strReturn = "Single"             ' 6
        Case dbDouble: strReturn = "Double"             ' 7
        Case dbDate: strReturn = "Date/Time"            ' 8
        Case dbBinary: strReturn = "Binary"             ' 9 (no interface)
        Case dbText                                     '10
            If (fld.Attributes And dbFixedField) = 0& Then
                strReturn = "Text"
            Else
                strReturn = "Text (fixed width)"        '(no interface)
            End If
        Case dbLongBinary: strReturn = "OLE Object"     '11
        Case dbMemo                                     '12
            If (fld.Attributes And dbHyperlinkField) = 0& Then
                strReturn = "Memo"
            Else
                strReturn = "Hyperlink"
            End If
        Case dbGUID: strReturn = "GUID"                 '15

        'Attached tables only: cannot create these in JET.
        Case dbBigInt: strReturn = "Big Integer"        '16
        Case dbVarBinary: strReturn = "VarBinary"       '17
        Case dbChar: strReturn = "Char"                 '18
        Case dbNumeric: strReturn = "Numeric"           '19
        Case dbDecimal: strReturn = "Decimal"           '20
        Case dbFloat: strReturn = "Float"               '21
        Case dbTime: strReturn = "Time"                 '22
        Case dbTimeStamp: strReturn = "Time Stamp"      '23

        'Constants for complex types don't work prior to Access 2007 and later.
        Case 101&: strReturn = "Attachment"         'dbAttachment
        Case 102&: strReturn = "Complex Byte"       'dbComplexByte
        Case 103&: strReturn = "Complex Integer"    'dbComplexInteger
        Case 104&: strReturn = "Complex Long"       'dbComplexLong
        Case 105&: strReturn = "Complex Single"     'dbComplexSingle
        Case 106&: strReturn = "Complex Double"     'dbComplexDouble
        Case 107&: strReturn = "Complex GUID"       'dbComplexGUID
        Case 108&: strReturn = "Complex Decimal"    'dbComplexDecimal
        Case 109&: strReturn = "Complex Text"       'dbComplexText
        Case Else: strReturn = "Field type " & fld.Type & " unknown"
    End Select

    FieldTypeName = strReturn
End Function
