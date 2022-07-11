GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetCompanyTemplateSectionByTemplateId') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetCompanyTemplateSectionByTemplateId
END
GO

CREATE Proc [dbo].[GetCompanyTemplateSectionByTemplateId]
(
@CompanyId int ,
@CompanyTemplateId int 
)
as 
begin

SELECT [CompanyTemplateSectionId]
      ,[CompanyTemplateId]
      ,[SectionType]
      ,[SectionName]
      ,[SectionBackgrounColor]
      ,[IsActive]
      ,[CreatedBy]
      ,[CreatedAt]
      ,[UpdatedBy]
      ,[UpdatedAt]
      ,[PrimaryText]
      ,[SecondaryText]
      ,[TertiaryText]
      ,[DisplayOrder]
      ,[SectionFor]
	  ,TSTM.[TemplateSectionName] AS SectionTypeName
  FROM [dbo].[CompanyTemplateSection] CTS
  JOIN [dbo].TemplateSectionTypeMaster  TSTM ON TSTM.TemplateSectionType = CTS.SectionType
  where IsActive = 1 
  AND CompanyTemplateId = @CompanyTemplateId
end
GO
