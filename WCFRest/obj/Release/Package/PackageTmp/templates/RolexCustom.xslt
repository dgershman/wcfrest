<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                              xmlns:diffgr="urn:schemas-microsoft-com:xml-diffgram-v1" 
                              exclude-result-prefixes="diffgr">  
  <xsl:output method="xml" omit-xml-declaration="no" />  
  <xsl:template match="/">  
    <articles>
      <xsl:for-each select="/DataTable/diffgr:diffgram/NewDataSet/Table/Story_Guid[not(.=preceding::Story_Guid)]">
        <xsl:variable name="CurrentStoryGuid" select="../Story_Guid" />
        <article>
          <xsl:attribute name="guid">
            <xsl:value-of select="../Story_Guid" />
          </xsl:attribute>
          <title>
            <xsl:value-of select="../Story_Title" />
          </title>
          <lastUpdatedEpoch>
            <xsl:value-of select="../Story_Updated_On_Epoch" />
          </lastUpdatedEpoch>
          <thumb1>
            <xsl:value-of select="../Story_Large_Thumbnail_Path" />
          </thumb1>
          <thumb2>
            <xsl:value-of select="../Story_Listing_Thumbnail_Path" />
          </thumb2>
          <material>
            <videos>
              <xsl:for-each select="/DataTable/diffgr:diffgram/NewDataSet/Table[Story_Guid=$CurrentStoryGuid and Asset_Type_Id='1']">
                <video>
                  <xsl:attribute name="guid">
                    <xsl:value-of select="Asset_Guid" />
                  </xsl:attribute>
                  <lastUpdatedEpoch>
                    <xsl:value-of select="Asset_Updated_On_Epoch"/>
                  </lastUpdatedEpoch>
                  <title>
                    <xsl:value-of select="Asset_Title"/>
                  </title>
                  <usageRights>
                    <xsl:value-of select="Rights_Usage_Details"/>
                  </usageRights>
                  <thumb1>
                    <xsl:value-of select="Asset_Small_Thumbnail_Path"/>
                  </thumb1>
                  <thumb2>
                    <xsl:value-of select="Asset_Large_Thumbnail_Path"/>
                  </thumb2>
                  <preview>
                    <xsl:value-of select="Asset_Preview_Path"/>
                  </preview>
                  <dlm>
                    <xsl:value-of select="DLMAPI"/>
                  </dlm>
                </video>
              </xsl:for-each>
            </videos>
            <audios>
              <xsl:for-each select="/DataTable/diffgr:diffgram/NewDataSet/Table[Story_Guid=$CurrentStoryGuid and Asset_Type_Id='2']">
                <audio>
                  <xsl:attribute name="guid">
                    <xsl:value-of select="Asset_Guid" />
                  </xsl:attribute>
                  <lastUpdatedEpoch>
                    <xsl:value-of select="Asset_Updated_On_Epoch"/>
                  </lastUpdatedEpoch>
                  <title>
                    <xsl:value-of select="Asset_Title"/>
                  </title>
                  <usageRights>
                    <xsl:value-of select="Rights_Usage_Details"/>
                  </usageRights>
                  <thumb1>
                    <xsl:value-of select="Asset_Small_Thumbnail_Path"/>
                  </thumb1>
                  <thumb2>
                    <xsl:value-of select="Asset_Large_Thumbnail_Path"/>
                  </thumb2>
                  <preview>
                    <xsl:value-of select="Asset_Preview_Path"/>
                  </preview>
                  <dlm>
                    <xsl:value-of select="DLMAPI"/>
                  </dlm>
                </audio>
              </xsl:for-each>
            </audios>
            <stills>
              <xsl:for-each select="/DataTable/diffgr:diffgram/NewDataSet/Table[Story_Guid=$CurrentStoryGuid and Asset_Type_Id='3']">
                <still>
                  <xsl:attribute name="guid">
                    <xsl:value-of select="Asset_Guid" />
                  </xsl:attribute>
                  <lastUpdatedEpoch>
                    <xsl:value-of select="Asset_Updated_On_Epoch"/>
                  </lastUpdatedEpoch>
                  <title>
                    <xsl:value-of select="Asset_Title"/>
                  </title>
                  <usageRights>
                    <xsl:value-of select="Rights_Usage_Details"/>
                  </usageRights>
                  <thumb1>
                    <xsl:value-of select="Asset_Small_Thumbnail_Path"/>
                  </thumb1>
                  <thumb2>
                    <xsl:value-of select="Asset_Large_Thumbnail_Path"/>
                  </thumb2>
                  <preview>
                    <xsl:value-of select="Asset_Preview_Path"/>
                  </preview>
                  <dlm>
                    <xsl:value-of select="DLMAPI"/>
                  </dlm>
                </still>
              </xsl:for-each>
            </stills>
            <documents>
              <xsl:for-each select="/DataTable/diffgr:diffgram/NewDataSet/Table[Story_Guid=$CurrentStoryGuid and Asset_Type_Id='4']">
                <document>
                  <xsl:attribute name="guid">
                    <xsl:value-of select="Asset_Guid" />
                  </xsl:attribute>
                  <lastUpdatedEpoch>
                    <xsl:value-of select="Asset_Updated_On_Epoch"/>
                  </lastUpdatedEpoch>
                  <title>
                    <xsl:value-of select="Asset_Title"/>
                  </title>
                  <usageRights>
                    <xsl:value-of select="Rights_Usage_Details"/>
                  </usageRights>
                  <thumb1>
                    <xsl:value-of select="Asset_Small_Thumbnail_Path"/>
                  </thumb1>
                  <thumb2>
                    <xsl:value-of select="Asset_Large_Thumbnail_Path"/>
                  </thumb2>
                  <preview>
                    <xsl:value-of select="Asset_Preview_Path"/>
                  </preview>
                  <language>
                    <xsl:value-of select="Language"/>
                  </language>
                  <dlm>
                    <xsl:value-of select="DLMAPI"/>
                  </dlm>
                </document>
              </xsl:for-each>
            </documents>
          </material>
        </article>
      </xsl:for-each>
    </articles>
  </xsl:template>
</xsl:stylesheet>