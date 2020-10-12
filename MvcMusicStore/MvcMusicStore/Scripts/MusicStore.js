function searchFailed() {
    $("#searchresults").html("Sorry, there was a problem with the search.")
}

$(function () {
    $("input[data-autocomplete-source]").each(function () {
        var target = $(this);
        target.autocomplete({
            source: target.attr("data-autocomplete-source")
        });
    });
})