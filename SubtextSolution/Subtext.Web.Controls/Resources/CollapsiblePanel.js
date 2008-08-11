// This script is embedded in the Subtext.Web.Controls assembly.

// Toggles the visibility of a CollapsiblePanel instance.
function ToggleVisible(targetID, imageID, linkImage, linkImageCollapsed)
{
	if (document.getElementById){
  		target = document.getElementById(targetID);
  		if (target.style.display == "none") {
  			target.style.display = "";
  		} else {
  			target.style.display = "none";
  		}
  		
  		if (linkImageCollapsed != "") {
  			image = document.getElementById(imageID);
  			if (target.style.display == "none") {
  				image.src = linkImageCollapsed;
  			} else {
  				image.src = linkImage;
  			}
  		}
  	}
}

