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

app.controller("auctionsCtrl", function ($scope, $http) {

    $scope.auctionList = "";
    $scope.auction = "";


    $http.get("/Auctions/GetAuctions")
        .success(function (result) {
            $scope.auctionList = result;
        })
        .error(function (result) {
            console.log(result);
        })

    $scope.auctionBid = function (auction) {

        debugger;
        $http.post("/Auctions/AuctionBid", { newBid: auction })
            .success(function (result) {
                debugger;
                $scope.auctionList = result;
            })
            .error(function (result) {
                console.log(result);
            })
    }


});
