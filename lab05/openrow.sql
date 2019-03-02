USE Lab01
GO

DECLARE @idoc int
DECLARE @doc xml
SELECT @doc = c FROM OPENROWSET(BULK 'C:\Users\user\Desktop\5\Базы Данных\ЛБ5\XML.xml',
                                SINGLE_BLOB) AS TEMP(c)
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc

SELECT *
FROM OPENXML (@idoc, '/GroupName/Group')
EXEC sp_xml_removedocument @idoc
