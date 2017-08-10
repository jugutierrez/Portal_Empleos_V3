window.fbAsyncInit = function () {
    FB.init({
        appId: '212466592535128',
        xfbml: true,
        version: 'v2.8'
    });
};






(function(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/es_LA/sdk.js#xfbml=1&version=v2.9&appId=212466592535128";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));