GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_SaveCompanySecretForTwillioNotificationService') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_SaveCompanySecretForTwillioNotificationService
END
GO
 
CREATE PROCEDURE [dbo].[SP_SaveCompanySecretForTwillioNotificationService]  
 @Id INT,  
 @CompanyId INT,  
 @ServiceName NVARCHAR(50),  
 @AccountSID NVARCHAR(MAX),  
 @AuthToken NVARCHAR(MAX),  
 @FromNumber NVARCHAR(225),  
 @SortCode NVARCHAR(225),  
 @APIKey NVARCHAR(200),  
 @SenderId NVARCHAR(50),  
 @URLLink NVARCHAR(MAX),  
 @SMTPServerAddress NVARCHAR(500),  
 @MailSendPort NVARCHAR(50),  
 @FromEmailId NVARCHAR(100),  
 @SMTPUserId NVARCHAR(500),  
 @SMTPPassword NVARCHAR(500),  
 @IsSSLEnabled BIT ,
 @RoboCallFromNumber NVARCHAR(500),
 @MessagingServiceSid NVARCHAR(MAX)
AS  
-- =============================================  
-- Author: Kuntal Das  
-- Create date: 01 January 2022  
-- Description:  Save CompanySecret For Twillio NotificationService  
-- =============================================  
BEGIN  
 IF(@Id=0)  
 BEGIN  
  INSERT INTO [dbo].[CompanySecret]  
      ([CompanyId]  
      ,[ServiceName]  
      ,[AccountSID]  
      ,[AuthToken]  
      ,[FromNumber]  
      ,[SortCode]  
      ,[APIKey]  
      ,[SenderId]  
      ,[URLLink]  
      ,[SMTPServerAddress]  
      ,[MailSendPort]  
      ,[FromEmailId]  
      ,[SMTPUserId]  
      ,[SMTPPassword]  
      ,[IsSSLEnabled]
	  ,[RoboCallFromNumber]
      ,[MessagingServiceSid])  
   VALUES  
      (@CompanyId  
      ,@ServiceName  
      ,@AccountSID  
      ,@AuthToken  
      ,@FromNumber  
      ,@SortCode  
      ,@APIKey  
      ,@SenderId  
      ,@URLLink  
      ,@SMTPServerAddress  
      ,@MailSendPort  
      ,@FromEmailId  
      ,@SMTPUserId  
      ,@SMTPPassword  
      ,@IsSSLEnabled
	  ,@RoboCallFromNumber
	  ,@MessagingServiceSid)  
      SET @Id=@@Identity  
  END  
  ELSE  
   BEGIN  
  IF(@ServiceName='TwillioService')  
   BEGIN  
    UPDATE [dbo].[CompanySecret]  
     SET [ServiceName] = @ServiceName  
     ,[AccountSID] = @AccountSID  
     ,[AuthToken] = @AuthToken  
     ,[FromNumber] = @FromNumber  
     ,[SortCode]=@SortCode
	 ,[RoboCallFromNumber]=@RoboCallFromNumber
     ,[MessagingServiceSid]=@MessagingServiceSid
      WHERE Id=@Id  
   END  
  ELSE IF(@ServiceName='SMSTemplate')  
   BEGIN  
    UPDATE [dbo].[CompanySecret]  
     SET [ServiceName] = @ServiceName  
     ,[APIKey] = @APIKey  
     ,[SenderId] = @SenderId  
     ,[URLLink] = @URLLink  
      WHERE Id=@Id  
   END  
  ELSE IF(@ServiceName='EmailTemplate')  
   BEGIN  
    UPDATE [dbo].[CompanySecret]  
     SET [ServiceName] = @ServiceName  
     ,[SMTPServerAddress]=@SMTPServerAddress  
      ,[MailSendPort]=@MailSendPort  
      ,[FromEmailId]=@FromEmailId  
      ,[SMTPUserId]=@SMTPUserId  
      ,[SMTPPassword]=@SMTPPassword  
      ,[IsSSLEnabled]=@IsSSLEnabled  
      WHERE Id=@Id  
   END  
   END  
   SELECT @Id AS Id  
END  
GO
