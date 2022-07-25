GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_GetCompanySecretForTwillioNotificationService') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_GetCompanySecretForTwillioNotificationService
END
GO
    
CREATE PROCEDURE [dbo].[SP_GetCompanySecretForTwillioNotificationService]  -- [dbo].[SP_GetCompanySecretForTwillioNotificationService] 8,''  
    @CompanyId INT,    
 @ServiceName NVARCHAR(50)    
AS    
-- =============================================    
-- Author: Kuntal Das    
-- Create date: 01 January 2022    
-- Description:  Get CompanySecret For TwillioNotificationService    
-- =============================================    
BEGIN  
if(isnull(@ServiceName,'') <> '')
begin
SELECT [Id],[CompanyId],[ServiceName],[AccountSID],[AuthToken],[FromNumber],[SortCode],[APIKey],[SenderId],[URLLink]    
   ,[SMTPServerAddress] ,[MailSendPort] ,[FromEmailId] ,[SMTPUserId] ,[SMTPPassword] ,ISNULL([IsSSLEnabled],0) [IsSSLEnabled],[RoboCallFromNumber],
   [MessagingServiceSid]  
  FROM [dbo].[CompanySecret] WHERE  CompanyId=@CompanyId AND ServiceName=@ServiceName    
end
else
begin
if((select count(*) from CompanySecret where ServiceName ='TwillioService' and  CompanyId=@CompanyId) >0)
begin
SELECT [Id],[CompanyId],[ServiceName],[AccountSID],[AuthToken],[FromNumber],[SortCode],[APIKey],[SenderId],[URLLink]    
   ,[SMTPServerAddress] ,[MailSendPort] ,[FromEmailId] ,[SMTPUserId] ,[SMTPPassword] ,ISNULL([IsSSLEnabled],0) [IsSSLEnabled],[RoboCallFromNumber],
   [MessagingServiceSid]  
  FROM [dbo].[CompanySecret] WHERE  CompanyId=@CompanyId AND ServiceName='TwillioService' 
end 
else
begin
SELECT [Id],[CompanyId],[ServiceName],[AccountSID],[AuthToken],[FromNumber],[SortCode],[APIKey],[SenderId],[URLLink]    
   ,[SMTPServerAddress] ,[MailSendPort] ,[FromEmailId] ,[SMTPUserId] ,[SMTPPassword] ,ISNULL([IsSSLEnabled],0) [IsSSLEnabled],[RoboCallFromNumber],
   [MessagingServiceSid]  
  FROM [dbo].[CompanySecret] WHERE  CompanyId=@CompanyId AND ServiceName='SMSTemplate' 
end
   
end
  
END
GO
