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
    $scope.findauction = function (auction) {

        debugger;
        $http.post("/Auctions/AuctionbyID", { id: auction })
            .success(function (result) {
                debugger;
                $scope.auctionList = result;
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
