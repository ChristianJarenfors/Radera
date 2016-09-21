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


//Auctions
//////////////////////////////////////////////////////////////
app.controller("auctionsCtrl", function ($scope, $http) {

    $scope.auctionList = [];
    $scope.auction = {};

    $scope.searchImmediate = searchImmediate;
    $scope.searchCategories = [];

    $scope.searchInput = {
        selectedCategory: {
            CategoryId: 0,
            CategoryName: ''
        },
        auctionName: ''
    };

    $http.get("/Auctions/GetAuctions")
        .success(function (result) {
            $scope.auctionList = result;
        })
        .error(function (result) {
            console.log(result);
        })

    //Get Categories
    $http.get("/Auctions/GetSearchCategories")
        .success(function (result) {
            debugger;
            $scope.searchCategories = result;
        })
        .error(function (result) {
            console.log(result);
        })


    //Custom Search filter
    function searchImmediate(item) {
        if (($scope.searchInput.selectedCategory.CategoryId == 0 ? true : $scope.searchInput.selectedCategory.CategoryId == item.Category.CategoryId) &&
            ($scope.searchInput.auctionName.length == 0 ? true : (item.Title.toLowerCase().indexOf($scope.searchInput.auctionName.toLowerCase()) >= 0))) {
            return true;
        }

        return false;
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
    $scope.isDetailsAreaVisible = false;
    $scope.categories = [];
   

    $http.get("/MyPage/GetAuctionsByUserId")
        .success(function (result) {
            $scope.myAuctionList = result;
        })
        .error(function (result) {
            console.log(result);
        })


    $http.get("/MyPage/GetCategories")
       .success(function (result) {
           debugger;
           $scope.categories = result;
       })
       .error(function (result) {
           console.log(result);
       })


    $scope.auctionDetails = function () {
        if ($scope.isDetailsAreaVisible === false) {
            $scope.isDetailsAreaVisible = true;
        }
        else {
            $scope.isDetailsAreaVisible = false;
        }
    }

    $scope.createAuction = function (myAuction) {

        $http.post("/MyPage/CreateAuction", { newAuction: myAuction })
            .success(function (result) {
                debugger;
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
