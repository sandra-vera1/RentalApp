// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let searchFilterbutton = document.getElementById("AdvancedSearchFilterButton");
let searchFilterDiv = document.getElementById("SearchFilterDiv");


searchFilterbutton.addEventListener("click", DisplayAdvancedSearchFilters);

function DisplayAdvancedSearchFilters()
{
    if (searchFilterDiv.style.display == "block") {
        searchFilterDiv.style.display = "none";
    }
    else {
        searchFilterDiv.style.display = "block";
    }


}