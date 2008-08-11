<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    <xsl:output method="html"/>
    <xsl:param name="applicationPath"/>

    <xsl:template match="/">
      <xsl:variable name="buildresults" select="//build/buildresults" />

      <script type="text/javascript">
         function toggleDiv(imgId, divId)
         {
            eDiv = document.getElementById(divId);
            eImg = document.getElementById(imgId);

            if ( eDiv.style.display == "none" )
            {
               eDiv.style.display="block";
               eImg.src="<xsl:value-of select="$applicationPath"/>/images/arrow_minus_small.gif";
            }
            else
            {
               eDiv.style.display = "none";
               eImg.src="<xsl:value-of select="$applicationPath"/>/images/arrow_plus_small.gif";
            }
         }
      </script>

      <div class="container">
         <span class="containerTitle">NAnt Timing Report</span>
         <span class="containerSubtitle">Total build time: <xsl:value-of select="//build/@buildtime"/></span>
         <div class="containerContents">
            <table class="section" rules="groups" cellpadding="2" cellspacing="0" border="0">
               <xsl:choose>
                  <xsl:when test="count($buildresults) > 0">
                     <xsl:apply-templates select="$buildresults" />
                  </xsl:when>
                  <xsl:otherwise>
                     <tr><td colspan="2">
                        <h2>Log does not contain any Xml output from NAnt.</h2>
                        <p>Please make sure that NAnt is executed using the XmlLogger (use the argument: <b>-logger:NAnt.Core.XmlLogger</b>).</p>
                     </td></tr>
                  </xsl:otherwise>
               </xsl:choose>
            </table>
         </div>
      </div>
   </xsl:template>
   
   <xsl:template match="buildresults">
      <thead>
         <tr class="header2">
            <th align="left">Target</th>
            <th align="right">Duration</th>
         </tr>
      </thead>
      <tbody>
         <xsl:apply-templates select="//target">
            <xsl:sort select="duration" order="descending" data-type="number" />          
         </xsl:apply-templates>
      </tbody>
   </xsl:template>
   
   <xsl:template match="target">
      <tr>     
         <td valign="top">
            <xsl:variable name="divId">
               <xsl:value-of select="generate-id()" />
            </xsl:variable>
            <img src="{$applicationPath}/images/arrow_plus_small.gif" alt="Toggle to see tasks in this target">
               <xsl:attribute name="id">
                  <xsl:text>img-</xsl:text>
                  <xsl:value-of select="$divId" />
               </xsl:attribute>
               <xsl:attribute name="onclick">toggleDiv('img-<xsl:value-of select="$divId" />','<xsl:value-of select="$divId" />')</xsl:attribute>
            </img>&#0160;
            <xsl:for-each select="ancestor::target"><xsl:value-of select="@name" />/</xsl:for-each>
            <xsl:value-of select="@name" />
            <div>
               <xsl:attribute name="id">
                  <xsl:value-of select="$divId" />
               </xsl:attribute>
               <xsl:attribute name="style">
                  <xsl:text>display:none;</xsl:text>
               </xsl:attribute>  
               <ul>
                  <xsl:apply-templates select="task">
                     <xsl:sort select="duration" order="descending" data-type="number" />          
                  </xsl:apply-templates>
               </ul>
            </div>
         </td>
         <td valign="top" align="right">
            <xsl:apply-templates select="duration" />
         </td>
      </tr>
   </xsl:template>
   
   <xsl:template match="task">
      <li><xsl:value-of select="@name" /> - <xsl:apply-templates select="duration" /></li>
   </xsl:template>

   <xsl:template match="duration">
      <xsl:variable name="hours" select="floor(node() div 3600000)" />
      <xsl:variable name="minutes" select="floor((node() mod 3600000) div 60000)" />
      <xsl:variable name="seconds" select="(node() mod 60000) div 1000" />

      <xsl:if test="$hours > 0"><xsl:value-of select="$hours" />:</xsl:if>
      <xsl:value-of select="format-number($minutes,'00')" />:<xsl:value-of select="format-number($seconds,'00.00')"/>
   </xsl:template>
</xsl:stylesheet>