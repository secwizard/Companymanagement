GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetTemplate') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetTemplate
END
GO
  
CREATE proc [dbo].[GetTemplate]  
@Url nvarchar(MAX)  
, @CompanyId int  
, @Type nvarchar(10)  
as  
begin  
 declare @CompanyTemplateId as int  
 if(@Url IS NOT NULL and @Url != '')  
 begin  
  set @CompanyTemplateId = (select top 1 CompanyTemplateId from CompanyTemplate where CompanyId = @CompanyId and Url = @Url and IsActive = 1 and [Type] = @Type)  
 end  
 else  
 begin  
  set @CompanyTemplateId = (select top 1 CompanyTemplateId from CompanyTemplate where CompanyId = @CompanyId and IsDefault = 1 and IsActive = 1 and [Type] = @Type)  
 end  
  
 select ct.CompanyTemplateId, ct.TemplateId, ct.TemplateName, ct.ViewName WebViewName, ct.MobileViewName, ct.PrimaryColor, ct.SecondaryColor, ct.TertiaryColor  
 , c.[Name], c.ImageFilePath CompanyLogo  
 from CompanyTemplate ct  
 --join FronEndTemplate t on t.TemplateId = ct.TemplateId  
 join Company c on c.CompanyId = ct.CompanyId  
 where ct.CompanyTemplateId = @CompanyTemplateId  
end  
GO
