<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:output method="html" indent="no" />
  <xsl:key name="betlimit-by-sporttype" match="betlimit/r" use="@sporttype" />
  <xsl:template name="betLimit">
    <xsl:choose>
      <xsl:when test="betlimit[@grouptype=158]">
        <tr class="headerDetail" onclick="toggle(this)">
          <td colspan="6">
            <span class="icon-chevron-down"></span>
            <xsl:value-of select="xslLogFormat:GetLogResource('BetLimit')" />
          </td>
        </tr>
        <tr>
          <td colspan="6">
            <table class="tblDesc">
              <xsl:for-each select="betlimit[@grouptype=158]/r[count(.|key('betlimit-by-sporttype',@sporttype)[1])=1]">
                <xsl:choose>
                  <xsl:when test="count(key('betlimit-by-sporttype', @sporttype)) &gt; 1">
                    <tr>
                      <td class="colW1">
                        <xsl:choose>
                          <xsl:when test="@sporttype=208 and @bettype=2101">
                            <xsl:value-of select="xslLogFormat:GetProductName(6)" />
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="xslLogFormat:GetSportName(@sporttype)" />
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                      <td class="colW2" colspan="5"></td>
                    </tr>
                    <xsl:for-each select="key('betlimit-by-sporttype', @sporttype)">
                      <tr>
                        <td class="colW2">
                          <xsl:value-of select="xslLogFormat:GetBetTypeName(@bettype)" />
                        </td>
                        <td class="colW2" colspan="5">
                          <xsl:value-of select="@stakelevellist" />
                        </td>
                      </tr>
                    </xsl:for-each>
                  </xsl:when>
                  <xsl:otherwise>
                    <tr>
                      <td class="colW1">
                        <xsl:choose>
                          <xsl:when test="@sporttype=208 and @bettype=2101">
                            <xsl:value-of select="xslLogFormat:GetProductName(6)" />
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="xslLogFormat:GetSportName(@sporttype)" />
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                      <td class="colW2" colspan="5">
                        <xsl:value-of select="@stakelevellist" />
                      </td>
                    </tr>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:for-each>
            </table>
          </td>
        </tr>
      </xsl:when>
      <xsl:when test="betlimit[@grouptype=159]">
        <tr class="headerDetail" onclick="toggle(this)">
          <td colspan="6">
            <span class="icon-chevron-down"></span>
            <xsl:value-of select="xslLogFormat:GetLogResource('BetLimit')" />
          </td>
        </tr>
        <tr>
          <td colspan="6">
            <table class="tblDesc">
              <xsl:for-each select="betlimit[@grouptype=159]/r[count(.|key('betlimit-by-sporttype',@sporttype)[1])=1]">
                <xsl:choose>
                  <xsl:when test="count(key('betlimit-by-sporttype', @sporttype)) &gt; 1">
                    <tr>
                      <td class="colW1">
                        <xsl:choose>
                          <xsl:when test="@sporttype=208 and @bettype=2101">
                            <xsl:value-of select="xslLogFormat:GetProductName(6)" />
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="xslLogFormat:GetSportName(@sporttype)" />
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                      <td class="colW2" colspan="5"></td>
                    </tr>
                    <xsl:for-each select="key('betlimit-by-sporttype', @sporttype)">
                      <tr>
                        <td class="colW2">
                          <xsl:value-of select="xslLogFormat:GetBetTypeName(@bettype)" />
                        </td>
                        <td class="colW2" colspan="5">
                          <xsl:value-of select="@stakelevellist" />
                        </td>
                      </tr>
                    </xsl:for-each>
                  </xsl:when>
                  <xsl:otherwise>
                    <tr>
                      <td class="colW1">
                        <xsl:choose>
                          <xsl:when test="@sporttype=208 and @bettype=2101">
                            <xsl:value-of select="xslLogFormat:GetProductName(6)" />
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="xslLogFormat:GetSportName(@sporttype)" />
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                      <td class="colW2" colspan="5">
                        <xsl:value-of select="@stakelevellist" />
                      </td>
                    </tr>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:for-each>
            </table>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>