<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:param name="IsInternal" select="0"></xsl:param>
  <xsl:output method="html" indent="no" />
  <xsl:key name="pt-by-sporttype" match="pt/r" use="@sporttype" />
  <xsl:key name="betlimit-by-sporttype" match="betlimit/r" use="@sporttype" />
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
              <xsl:if test="discount[@grouptype=150]">
                <tr class="headerDetail" onclick="toggle(this)">
                  <td colspan="6">
                    <span class="icon-chevron-down"></span>
                    <xsl:value-of select="xslLogFormat:GetLogResource('Commision')" />
                  </td>
                </tr>
                <tr>
                  <td colspan="6">
                    <table class="tblDesc">
                      <tr>
                        <xsl:apply-templates select="discount/r[position() &lt; 4]" />
                      </tr>
                      <tr>
                        <xsl:apply-templates select="discount/r[position() &gt;= 4]" />
                      </tr>
                    </table>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="betsetting[@grouptype=151]">
                <tr class="headerDetail" onclick="toggle(this)">
                  <td colspan="6">
                    <span class="icon-chevron-down"></span>
                    <xsl:value-of select="xslLogFormat:GetLogResource('BetSetting')" />
                  </td>
                </tr>
                <tr>
                  <td colspan="6">
                    <table class="tblDesc">
                      <tr class="headerSportTypeDetail" style="text-align:center;">
                        <td>
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('MinBet')" />
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('MaxBet')" />
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('MaxPerMatch')" />
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('MaxPayoutPerMatch')" />
                        </td>
                      </tr>
                      <xsl:for-each select="betsetting/r">
                        <tr>
                          <td class="colW1">
                            <xsl:value-of select="xslLogFormat:GetSportsBookBetSettingName(@sporttype,@bettype)" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@minbet" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@maxbet" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@mpm" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@maxpayout" />
                          </td>
                        </tr>
                      </xsl:for-each>
                    </table>
                  </td>
                </tr>
              </xsl:if>

              <xsl:if test="discount[@grouptype=156]">
                <tr class="headerDetail" onclick="toggle(this)">
                  <td colspan="6">
                    <span class="icon-chevron-down"></span>
                    <xsl:value-of select="xslLogFormat:GetLogResource('Commision')" />
                  </td>
                </tr>
                <tr>
                  <td colspan="6">
                    <table class="tblDesc tbl-last">
                      <tr>
                        <xsl:for-each select="discount/r[position() &lt; 4]">
                          <td class="colW1">
                            <xsl:value-of select="xslLogFormat:GetSportName(@sporttype)" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@discount" />
                          </td>
                        </xsl:for-each>
                      </tr>
                      <tr>
                        <xsl:for-each select="discount/r[position() &gt;= 4 and position() &lt; 7]">
                          <td class="colW1">
                            <xsl:value-of select="xslLogFormat:GetSportName(@sporttype)" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@discount" />
                          </td>
                        </xsl:for-each>
                      </tr>
                      <tr>
                        <xsl:for-each select="discount/r[position() &gt;= 7]">
                          <td class="colW1">
                            <xsl:value-of select="xslLogFormat:GetSportName(@sporttype)" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@discount" />
                          </td>
                          <xsl:if test="position()=last() and position() mod 3 &gt; 0">
                            <td class="colW1">
                            </td>
                            <td class="colW2">
                            </td>
                          </xsl:if>
                        </xsl:for-each>
                      </tr>
                    </table>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="betsetting[@grouptype=154]">
                <tr class="headerDetail" onclick="toggle(this)">
                  <td colspan="6">
                    <span class="icon-chevron-down"></span>
                    <xsl:value-of select="xslLogFormat:GetLogResource('BetSetting')" />
                  </td>
                </tr>
                <tr>
                  <td colspan="6">
                    <table class="tblDesc">
                      <tr style="text-align:center;" class="headerSportTypeDetail">
                        <td>
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('MinBet')" />
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('MaxBet')" />
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('MaxPerMatch')" />
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('MaxPerBall')" />
                        </td>
                      </tr>
                      <xsl:for-each select="betsetting/r">
                        <tr>
                          <td class="colW1">
                            <xsl:value-of select="xslLogFormat:GetRNGGamesBetSettingName(@sporttype,@bettype)" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@minbet" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@maxbet" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@mpm" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@maxpernumber" />
                          </td>
                        </tr>
                      </xsl:for-each>
                    </table>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="parlaymaxpayout">
                <tr class="headerDetail" onclick="toggle(this)">
                  <td colspan="6">
                    <span class="icon-chevron-down"></span>
                    <xsl:value-of select="xslLogFormat:GetLogResource('ParlayMaxPayout')" />
                  </td>
                </tr>
                <tr>
                  <td colspan="6">
                    <table class="tblDesc">
                      <tr style="text-align:center;" class="headerSportTypeDetail">
                        <td></td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('SingleMatchParlay')" />
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('Combo2')" />
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('Combo35')" />
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('ComboOver6')" />
                        </td>
                      </tr>
                      <xsl:for-each select="parlaymaxpayout/r">
                        <tr>
                          <td class="colW1">
                            <xsl:value-of select="xslLogFormat:GetParlayMaxPayoutSportBetTypeName(@sporttype,@bettype)" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@singlemp" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@combo2" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@combo35" />
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@combo6" />
                          </td>
                        </tr>
                      </xsl:for-each>
                    </table>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="pt[@grouptype=153]">
                <tr class="headerDetail" onclick="toggle(this)">
                  <td colspan="6">
                    <span class="icon-chevron-down"></span>
                    <xsl:value-of select="xslLogFormat:GetLogResource('PositionTaking')" />
                  </td>
                </tr>
                <tr>
                  <td colspan="6">
                    <table class="tblDesc tbl-last">
                      <xsl:choose>
                        <xsl:when test="$groupid=1">
                          <xsl:for-each select="pt/r[count(. | key('pt-by-sporttype', @sporttype)[1]) = 1]">
                            <tr class="headerSportTypeDetail">
                              <td>
                                <xsl:value-of select="xslLogFormat:GetSportName(@sporttype)" />
                              </td>
                              <td style="text-align:center;">
                                <xsl:value-of select="xslLogFormat:GetLogResource('lblPresetPTSuper')" />
                              </td>
                              <td style="text-align:center;">
                                <xsl:value-of select="xslLogFormat:GetLogResource('isSuperAutoPT')" />
                              </td>
                              <td style="text-align:center;">
                                <xsl:value-of select="xslLogFormat:GetLogResource('lblAutoPTSuper')" />
                              </td>
                              <td style="text-align:center;">
                                <xsl:value-of select="xslLogFormat:GetLogResource('lblMaxPTMaster')" />
                              </td>
                              <td style="text-align:center;">
                                <xsl:value-of select="xslLogFormat:GetLogResource('lblMinPTMaster')" />
                              </td>
                            </tr>
                            <xsl:for-each select="key('pt-by-sporttype', @sporttype)">
                              <xsl:if test="@sporttype=99 and @bettype=9">
                                <tr class="headerSportTypeDetail">
                                  <td>
                                    <xsl:value-of select="xslLogFormat:GetSportsBookBetSettingName(@sporttype,@bettype)" />
                                  </td>
                                  <td style="text-align:center;">
                                    <xsl:value-of select="xslLogFormat:GetLogResource('lblPresetPTSuper')" />
                                  </td>
                                  <td style="text-align:center;">
                                    <xsl:value-of select="xslLogFormat:GetLogResource('isSuperAutoPT')" />
                                  </td>
                                  <td style="text-align:center;">
                                    <xsl:value-of select="xslLogFormat:GetLogResource('lblAutoPTSuper')" />
                                  </td>
                                  <td style="text-align:center;">
                                    <xsl:value-of select="xslLogFormat:GetLogResource('lblMaxPTMaster')" />
                                  </td>
                                  <td style="text-align:center;">
                                    <xsl:value-of select="xslLogFormat:GetLogResource('lblMinPTMaster')" />
                                  </td>
                                </tr>
                              </xsl:if>
                              <tr>
                                <td class="colW1">
                                  <xsl:choose>
                                    <xsl:when test="@islive=1">
                                      <xsl:value-of select="concat(xslLogFormat:GetBetTypeName(@bettype),' (',xslLogFormat:GetLogResource('Live'),')')" />
                                    </xsl:when>
                                    <xsl:otherwise>
                                      <xsl:value-of select="xslLogFormat:GetBetTypeName(@bettype)" />
                                    </xsl:otherwise>
                                  </xsl:choose>
                                </td>
                                <td class="colW2">
                                  <xsl:value-of select="@superpt" />
                                </td>
                                <td class="colW2">
                                  <xsl:value-of select="@isauto" />
                                </td>
                                <td class="colW2">
                                  <xsl:value-of select="@autopt" />
                                </td>
                                <td class="colW2">
                                  <xsl:value-of select="@masterpt" />
                                </td>
                                <td class="colW2">
                                  <xsl:value-of select="@minpt" />
                                </td>
                              </tr>
                            </xsl:for-each>
                          </xsl:for-each>
                        </xsl:when>
                        <xsl:otherwise>
                          <tr style="text-align:center;" class="headerSportTypeDetail">
                            <td></td>
                            <td>
                              <xsl:value-of select="xslLogFormat:GetLogResource('lblPresetPTSuper')" />
                            </td>
                            <td>
                              <xsl:value-of select="xslLogFormat:GetLogResource('isSuperAutoPT')" />
                            </td>
                            <td>
                              <xsl:value-of select="xslLogFormat:GetLogResource('lblAutoPTSuper')" />
                            </td>
                            <td>
                              <xsl:value-of select="xslLogFormat:GetLogResource('lblMaxPTMaster')" />
                            </td>
                            <td>
                              <xsl:value-of select="xslLogFormat:GetLogResource('lblMinPTMaster')" />
                            </td>
                          </tr>
                          <xsl:for-each select="pt/r">
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
                              <td class="colW2">
                                <xsl:value-of select="@superpt" />
                              </td>
                              <td class="colW2">
                                <xsl:value-of select="@isauto" />
                              </td>
                              <td class="colW2">
                                <xsl:value-of select="@autopt" />
                              </td>
                              <td class="colW2">
                                <xsl:value-of select="@masterpt" />
                              </td>
                              <td class="colW2">
                                <xsl:value-of select="@minpt" />
                              </td>
                            </tr>
                          </xsl:for-each>
                        </xsl:otherwise>
                      </xsl:choose>
                    </table>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="winlosslimit[@grouptype=155]">
                <tr class="headerDetail" onclick="toggle(this)">
                  <td colspan="6">
                    <span class="icon-chevron-down"></span>
                    <xsl:value-of select="xslLogFormat:GetLogResource('WinLossLimit')" />
                  </td>
                </tr>
                <tr>
                  <td colspan="6">
                    <table class="tblDesc">
                      <tr style="text-align:center;" class="headerSportTypeDetail">
                        <td></td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('MaximumWin')" />
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('MaximumLoss')" />
                        </td>
                        <td>
                          <xsl:value-of select="xslLogFormat:GetLogResource('DailyAutoReset')" />
                        </td>
                      </tr>
                      <xsl:for-each select="winlosslimit/r">
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
                          <td class="colW2">
                            <xsl:choose>
                              <xsl:when test="@maxwinlimit=0">
                                <xsl:value-of select="xslLogFormat:GetLogResource('Unlimited')" />
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="@maxwinlimit" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td class="colW2">
                            <xsl:choose>
                              <xsl:when test="@maxlosslimit=0">
                                <xsl:value-of select="xslLogFormat:GetLogResource('Unlimited')" />
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="@maxlosslimit" />
                              </xsl:otherwise>
                            </xsl:choose>
                          </td>
                          <td class="colW2">
                            <xsl:value-of select="@isautoreset" />
                          </td>
                        </tr>
                      </xsl:for-each>
                    </table>
                  </td>
                </tr>
              </xsl:if>
              <xsl:if test="betlimit">
                <tr class="headerDetail" onclick="toggle(this)">
                  <td colspan="6">
                    <span class="icon-chevron-down"></span>
                    <xsl:value-of select="xslLogFormat:GetLogResource('BetLimit')" />
                  </td>
                </tr>
                <tr>
                  <td colspan="6">
                    <table class="tblDesc">
                      <xsl:if test="betlimit[@grouptype=158]">
                        <tr class="headerSportTypeDetail">
                          <td colspan="6">
                            <xsl:value-of select="xslLogFormat:GetProductName(41)" />
                          </td>
                        </tr>
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
                      </xsl:if>
                      <xsl:if test="betlimit[@grouptype=159]">
                        <tr class="headerSportTypeDetail">
                          <td colspan="6">
                            <xsl:value-of select="xslLogFormat:GetProductName(53)" />
                          </td>
                        </tr>
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
                      </xsl:if>
                    </table>
                  </td>
                </tr>
              </xsl:if>
            </table>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>

  <xsl:template match="discount/r">
    <td class="colW1">
      <xsl:value-of select="xslLogFormat:GetSportbookCommissionName(@sporttype,@bettype)" />
    </td>
    <td class="colW2">
      <xsl:value-of select="@discount" />
    </td>
  </xsl:template>
</xsl:stylesheet>