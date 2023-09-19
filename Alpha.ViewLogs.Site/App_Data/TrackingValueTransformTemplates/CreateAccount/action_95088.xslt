<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:include href="./SportsBook/commission.xslt" />
  <xsl:include href="./SportsBook/betSetting.xslt" />
  <xsl:include href="./SportsBook/parlayMaxPayout.xslt" />
  <xsl:include href="./RNGGames/betSetting.xslt" />
  <xsl:include href="./PositionTaking/master.xslt" />
  <xsl:include href="./commission.xslt" />
  <xsl:include href="./winLossLimit.xslt" />
  <xsl:include href="./betLimit.xslt" />
  <xsl:param name="IsInternal" select="0"></xsl:param>
  <xsl:output method="html" indent="no" />
  <xsl:template match="root">
    <table class="tblDesc">
      <xsl:for-each select="group">
        <xsl:variable name="groupid" select="@id" />
        <tr class="headerGroup" onclick="toggle(this)">
          <xsl:attribute name="class">
            <xsl:choose>
              <xsl:when test="$groupid=1">
                <xsl:value-of select="string('headerGroup ')" />
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="string('headerGroup toggle-icon')" />
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>

          <td colspan="6">
            <span class="icon-chevron-down"></span>
            <xsl:value-of select="xslLogFormat:GetProductGroupName(@id)" />
          </td>
        </tr>
        <tr>
          <td colspan="6">
            <xsl:variable name="style">
              <xsl:choose>
                <xsl:when test="$groupid=1">
                  <xsl:value-of select="title" />
                </xsl:when>
              </xsl:choose>
            </xsl:variable>
            <table class="tblDesc tbl-last">
              <xsl:if test="$groupid!=1">
                <xsl:attribute name="style">
                  <xsl:value-of select="'display:none;'" />
                </xsl:attribute>
              </xsl:if>
              <xsl:apply-templates select="discount" />
              <xsl:apply-templates select="betsetting" />
              <xsl:apply-templates select="parlaymaxpayout" />
              <xsl:call-template name="masterPT">
                <xsl:with-param name="groupid" select="$groupid" />
              </xsl:call-template>
              <xsl:apply-templates select="winlosslimit" />
              <xsl:call-template name="betLimit" />
            </table>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>