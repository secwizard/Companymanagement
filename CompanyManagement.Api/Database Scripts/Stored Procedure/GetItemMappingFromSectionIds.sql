GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetItemMappingFromSectionIds') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetItemMappingFromSectionIds
END
GO
CREATE Proc [dbo].[GetItemMappingFromSectionIds]
(
@CompanyTemplateSectionIds nvarchar(max) 
)
as 
begin
SELECT [CompanyTemplateSectionItemMappingId]
      ,[CompanyTemplateSectionId]
      ,[ItemId]
      ,[VariantId]
      ,[PrimaryText]
      ,[SecondaryText]
      ,[TertiaryText]
      ,[DisplayOrder]
      ,[IsActive]
      ,[CreatedBy]
      ,[CreatedAt]
      ,[UpdatedBy]
      ,[UpdatedAt]
  FROM [dbo].[CompanyTemplateSectionItemMapping]

  where IsActive = 1 
  AND CompanyTemplateSectionId in (select cast(value as int) from split(',',@CompanyTemplateSectionIds))
end
GO
