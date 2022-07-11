GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_SaveSendNotificationLog') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_SaveSendNotificationLog
END
GO
-- =============================================
-- Author:		KUNTAL DAS
-- Create date: 25 JAN 2022
-- Description:	SAVE SendNotificationLog Data
-- =============================================
CREATE PROCEDURE [dbo].[SP_SaveSendNotificationLog]
@JsonSendData VARCHAR(MAX),
@Status BIT,
@Message VARCHAR(MAX),
@CompanyId INT,
@UserId UNIQUEIDENTIFIER
AS
BEGIN
INSERT INTO [dbo].[SendNotificationLog]
           ([JsonSendData]
           ,[Status]
           ,[Message]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[IsActive]
		   ,[CompanyId])
     VALUES
           (@JsonSendData
           ,@Status
           ,@Message
           ,@UserId
           ,getdate()
           ,1
		   ,@CompanyId)
		   SELECT CAST(@@Identity AS INT) SendNotificationLogId
END
GO
