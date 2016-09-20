//app.js

var app = angular.module("app", ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider.

        when('/index', {
            templateUrl: 'angular/views/home/index.html'
        }).
        when('/about', {
            templateUrl: 'angular/views/home/about.html'
        }).
        when('/contact', {
            templateUrl: 'angular/views/home/contact.html'
        })
        .when('/auctions', {
            templateUrl: 'angular/views/home/auctions.html'
        })
        .when('/mypage', {
            templateUrl: 'angular/views/inlogged/mypage.html'
        })
        .when('/auctionsinlogged', {
            templateUrl: 'angular/views/inlogged/auctions.html'
        })
        .when('/bid', {
            templateUrl: 'angular/views/inlogged/bid.html'
        })
});


//Actions
//////////////////////////////////////////////////////////////
app.controller("auctionsCtrl", function ($scope, $http) {

    $scope.auctionList = "";
    $scope.auction = {};


    $http.get("/Auctions/GetAuctions")
        .success(function (result) {
            $scope.auctionList = result;
        })
        .error(function (result) {
            console.log(result);
        })

    $scope.openTab = function (evt, AuctionID, wind) {
        // Declare all variables
        var i, tabcontent, tablinks;
        
        // Get all elements with class="tabcontent" and hide them
        tabcontent = document.getElementsByClassName("tabcontent");
        for (i = 0; i < tabcontent.length; i++) {
            tabcontent[i].style.display = "none";
        }

        // Get all elements with class="tablinks" and remove the class "active"
        tablinks = document.getElementsByClassName("tablinks");
        for (i = 0; i < tablinks.length; i++) {
            tablinks[i].className = tablinks[i].className.replace(" active", "");
        }
        var windowid = wind + AuctionID
        //alert(windowid);
        // Show the current tab, and add an "active" class to the link that opened the tab
        document.getElementById(windowid).style.display = "block";
        evt.currentTarget.className += " active";
    }
    //$scope.auctionBid = function (auction) {
        
    //    $http.post("/Auctions/AuctionBid", { newBid: auction })
    //        .success(function (result) {
    //            $scope.auctionList = result;
    //        })
    //        .error(function (result) {
    //            console.log(result);
    //        })
    //}

});


//MyActions
//////////////////////////////////////////////////////////////

app.controller("myAuctionsCtrl", function ($scope, $http) {

    $scope.myAuctionList = "";
    $scope.myAuction = "";

   
    $http.get("/MyPage/GetAuctionsByUserId")
        .success(function (result) {
            $scope.myAuctionList = result;
        })
        .error(function (result) {
            console.log(result);
        })


    $scope.createAuction = function (myAuction) {

        $http.post("/MyPage/CreateAuction", { newAuction: myAuction })
            .success(function (result) {
                $scope.myAuctionList = result;
                $scope.myAuction = "";
            })
            .error(function (result) {
                console.log(result);
            })
    }


    $scope.selectAuctionById = function (id) {
        
        $http.post("/MyPage/SelectAuctionById", { id: id })
            .success(function (result) {
                $scope.myAuction = result;
            })
            .error(function (result) {
                console.log(result);
            })
    }

    $scope.updateAuction = function (auction) {
        
        $http.post("/MyPage/UpdateAuction", { auction: auction })
            .success(function (result) {
                $scope.myAuctionList = result;
                $scope.myAuction = "";
            })
            .error(function (result) {
                console.log(result);
            })
    }

    $scope.deleteAuction = function (id) {

        $http.post("/MyPage/DeleteAuction", { id: id })
            .success(function (result) {
                $scope.myAuctionList = result;
            })
            .error(function (result) {
                console.log(result);
            })
    }

});
