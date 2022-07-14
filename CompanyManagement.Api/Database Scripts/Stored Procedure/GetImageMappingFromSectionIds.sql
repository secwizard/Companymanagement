GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetImageMappingFromSectionIds') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetImageMappingFromSectionIds
END
GO

CREATE Proc [dbo].[GetImageMappingFromSectionIds]
(
@CompanyTemplateSectionIds nvarchar(max) 
)
as 
begin

SELECT [CompanyTemplateSectionImageMappingId]
      ,[CompanyTemplateSectionId]
      ,[ImagePath]
      ,[DisplayOrder]
      ,[IsActive]
      ,[CreatedBy]
      ,[CreatedAt]
      ,[UpdatedBy]
      ,[UpdatedAt]
      ,[ItemId]
  FROM [dbo].[CompanyTemplateSectionImageMapping]
  where IsActive = 1 
  AND CompanyTemplateSectionId in (select cast(value as int) from split(',',@CompanyTemplateSectionIds))
end
GO
