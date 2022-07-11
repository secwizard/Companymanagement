GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetCompanyMailInfo') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetCompanyMailInfo
END
GO
  
-- =============================================  
-- Author:  Rajib Mukhopadhyay  
-- Create date: 19 Feb 2021  
-- Description: Get company mail info  
-- =============================================  
CREATE PROCEDURE [dbo].[GetCompanyMailInfo]  -- [dbo].[GetCompanyMailInfo] 8
(  
 @CompanyId Bigint  
)  
AS  
--DECLARE @CompanyId Bigint = 1  
BEGIN  
 SET NOCOUNT ON;  
  
 SELECT  
    CompanyId = c.[CompanyId]  
  , CompImageFilePath = c.[ImageFilePath]  
  , CompLogoName = c.[LogoFileName]  
  , CompName = c.[Name]  
  , CompAdminEmail = cs.FromEmailId
  , CompCustServiceTel =  c.[ServicePhone]  
  , SMTPServer =  cs.SMTPServerAddress  
  , FromEmailDisplayName =  m.[FromEmailDisplayName]  
  , FromEmailId =  cs.SMTPUserId 
  , FromEmailPwd =  cs.SMTPPassword  
  , [Ssl] =  cs.IsSSLEnabled
  , [Port] = CONVERT(int,cs.MailSendPort) 
  , GSTIN=ISNULL(GSTNumber,'')  
  , CurrencySymbol = (select top 1 CurrencySymbol from CurrencyMaster where CurrencyId = c.CurrencyId)  
  , CompTermsConditionOrder = (SELECT TOP 1 [HTMLData] FROM [dbo].[Template] WHERE [TemplateType]='TermsCondition' AND [Name]='ORDER' AND CompanyId = @CompanyId AND IsActive =1)  
  , CompTermsConditionInvoice = (SELECT TOP 1 [HTMLData] FROM [dbo].[Template] WHERE [TemplateType]='TermsCondition' AND [Name]='INVOICE' AND CompanyId = @CompanyId AND IsActive =1)  
  , CompanyTermsConditionPayment = (SELECT TOP 1 [HTMLData] FROM [dbo].[Template] WHERE [TemplateType]='TermsCondition' AND [Name]='PAYMENT' AND CompanyId = @CompanyId AND IsActive =1)  
  , OrderEmailTemplate = (SELECT TOP 1 [HTMLData] FROM [dbo].[Template] WHERE [TemplateType]='EMAIL' AND [Name]='ORDER' AND CompanyId = @CompanyId AND IsActive =1)  
  , InvoiceEmailTemplate = (SELECT TOP 1 [HTMLData] FROM [dbo].[Template] WHERE [TemplateType]='EMAIL' AND [Name]='INVOICE' AND CompanyId = @CompanyId AND IsActive =1)  
  , ReminderEmailTemplate = (SELECT TOP 1 [HTMLData] FROM [dbo].[Template] WHERE [TemplateType]='Reminder' AND [Name]='StockReminder' AND CompanyId = @CompanyId AND IsActive =1)  
 FROM [dbo].[Company] c  
 INNER JOIN [dbo].MailServer m ON c.CompanyId = m.CompanyId   
 INNER JOIN [dbo].CompanySecret cs ON c.CompanyId = cs.CompanyId   and ServiceName = 'EmailTemplate'
 AND c.CompanyId = @CompanyId   
 AND c.IsActive = 1  
END  
GO
