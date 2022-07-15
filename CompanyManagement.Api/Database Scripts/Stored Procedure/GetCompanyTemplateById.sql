GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetCompanyTemplateById') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetCompanyTemplateById
END
GO

CREATE Proc [dbo].[GetCompanyTemplateById]
(
@CompanyId int ,
@CompanyTemplateId int 
)
as 
begin

SELECT [CompanyTemplateId]
      ,[CompanyId]
      ,[TemplateId]
      ,[TemplateName]
      ,[IsDefault]
      ,[Url]
      ,[PrimaryColor]
      ,[SecondaryColor]
      ,[TertiaryColor]
      ,[IsActive]
      ,[CreatedBy]
      ,[CreatedAt]
      ,[UpdatedBy]
      ,[UpdatedAt]
      ,[Type]
      ,[IsForB2C]
      ,[TemplateView]
      ,[ViewName]
      ,[MobileViewName]
      ,[ImagePath]
      ,[OnlyForMobile]
      ,[IsEditable]
      ,[TopBarBackgroundColor]
      ,[TopLogoUrl]
      ,[TopCartIconUrl]
      ,[TopCartIconBackgroundColor]
      ,[TopProfileIconUrl]
      ,[TopProfileIconBackgroundColor]
      ,[TopMenuIconUrl]
      ,[PageBackgroundColor]
      ,[FontBackgroundBrushColor]
      ,[FontFamilyId]
      ,[GeneralFontColor]
      ,[SeeAllArrowIconUrl]
      ,[ShopNowFontColor]
      ,[ShopNowBackgroundColor]
      ,[ShopNowBorderRadius]
      ,[ShopNowBorderColor]
      ,[SectionBorderRadius]
      ,[SubSectionBorderRadius]
      ,[IsSubSectionTransparent]
      ,[SubSectionGradientPrimaryColor]
      ,[SubSectionGradientSecondaryColor]
      ,[LargeBrushName]
      ,[MediumBrushName]
      ,[SmallBrushName]
  FROM [dbo].[CompanyTemplate]
 where IsActive = 1 
  AND CompanyTemplateId = @CompanyTemplateId
end
GO
