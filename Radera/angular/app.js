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
        .when('/auctiondetails/:id', {
            controller: 'auctionDetailsController',
            templateUrl: 'angular/views/inlogged/auctiondetails.html'
        })
        .when('/auctionsinlogged', {
            templateUrl: 'angular/views/inlogged/auctions.html'
        })
        .when('/admin', {
            templateUrl: 'angular/views/admin/adminpage.html'
        })
        .when('/handleusers', {
            templateUrl: 'angular/views/admin/handleusers.html'
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

    $scope.placeBid = function (bid, auction) {
        $http.post("/Auctions/placeBid", { nyBid: bid, thisAuction: auction })
            .success(function (result) {
                $scope.auctionList = result;
            })
            .error(function (result) {
                console.log(result);
            })
    }

    $scope.postcomment = function (message, auction) {
        $http.post("/Auctions/postComment", { theMessage: message, thisAuction: auction })
            .success(function (result) {
                $scope.auctionList = result;
            })
            .error(function (result) {
                console.log(result);
            })
    }
    $scope.openTabUnlogged = function (evt, AuctionID, wind) {
        // Declare all variables
        var i, tabcontent, tablinks;

        // Get all elements with class="tabcontent" and hide them
        //tabcontent = document.getElementsByClassName("tabcontent");
        //for (i = 0; i < tabcontent.length; i++) {
        //    tabcontent[i].style.display = "none";
        //}
        var windowid = 'main' + AuctionID;
        document.getElementById(windowid).style.display = "none";
        document.getElementById(windowid).className = document.getElementById(windowid).className.replace(" active", "");
        var windowid = 'comments' + AuctionID;
        document.getElementById(windowid).style.display = "none";
        document.getElementById(windowid).className = document.getElementById(windowid).className.replace(" active", "");
        //var windowid = 'comment' + AuctionID;
        //document.getElementById(windowid).style.display = "none";
        //document.getElementById(windowid).className = document.getElementById(windowid).className.replace(" active", "");
        // Get all elements with class="tablinks" and remove the class "active"
        //tablinks = document.getElementsByClassName("tablinks");
        //for (i = 0; i < tablinks.length; i++) {
        //    tablinks[i].className = tablinks[i].className.replace(" active", "");
        //}
        windowid = wind + AuctionID;
        //alert(windowid);
        // Show the current tab, and add an "active" class to the link that opened the tab
        document.getElementById(windowid).style.display = "block";
        evt.currentTarget.className += " active";
    }
    $scope.openTab = function (evt, AuctionID, wind) {
        // Declare all variables
        var i, tabcontent, tablinks;

        // Get all elements with class="tabcontent" and hide them
        //tabcontent = document.getElementsByClassName("tabcontent");
        //for (i = 0; i < tabcontent.length; i++) {
        //    tabcontent[i].style.display = "none";
        //}
        var windowid = 'main' + AuctionID;
        document.getElementById(windowid).style.display = "none";
        document.getElementById(windowid).className = document.getElementById(windowid).className.replace(" active", "");
        var windowid = 'comments' + AuctionID;
        document.getElementById(windowid).style.display = "none";
        document.getElementById(windowid).className = document.getElementById(windowid).className.replace(" active", "");
        var windowid = 'comment' + AuctionID;
        document.getElementById(windowid).style.display = "none";
        document.getElementById(windowid).className = document.getElementById(windowid).className.replace(" active", "");
        // Get all elements with class="tablinks" and remove the class "active"
        //tablinks = document.getElementsByClassName("tablinks");
        //for (i = 0; i < tablinks.length; i++) {
        //    tablinks[i].className = tablinks[i].className.replace(" active", "");
        //}
        windowid = wind + AuctionID;
        //alert(windowid);
        // Show the current tab, and add an "active" class to the link that opened the tab
        document.getElementById(windowid).style.display = "block";
        evt.currentTarget.className += " active";
    }

    //Get Categories
    $http.get("/Auctions/GetSearchCategories")
        .success(function (result) {
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


//Admin CRUD
//////////////////////////////////////////////////////////////

app.controller("adminAuctionsCtrl", function ($scope, $http) {

    $scope.auctionList = [];
    $scope.adminAuction = "";
    $scope.isDetailsAreaVisible = false;
    $scope.categories = [];

    $scope.searchImmediate = searchImmediate;
    $scope.searchCategories = [];

    $scope.searchInput = {
        selectedCategory: {
            CategoryId: 0,
            CategoryName: ''
        },
        auctionName: ''
    };


    $http.get("/Admin/GetSearchCategories")
        .success(function (result) {
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

    $scope.auctionDetails = function () {
        if ($scope.isDetailsAreaVisible === false) {
            $scope.isDetailsAreaVisible = true;
        }
        else {
            $scope.isDetailsAreaVisible = false;
        }
    }

    $http.get("/Admin/GetCategories")
       .success(function (result) {
           $scope.categories = result;
       })
       .error(function (result) {
           console.log(result);
       })

    $http.get("/Admin/GetAuctions")
            .success(function (result) {
                $scope.auctionList = result;
            })
            .error(function (result) {
                console.log(result);
            })


    $scope.selectAuctionById = function (id) {
        $http.post("/Admin/SelectAuctionById", { id: id })
            .success(function (result) {
                $scope.adminAuction = result;
            })
            .error(function (result) {
                console.log(result);
            })
    }

    $scope.updateAuction = function (auction) {
        $http.post("/Admin/UpdateAuction", { auction: auction })
            .success(function (result) {
                $scope.auctionList = result;
                $scope.adminAuction = "";
            })
            .error(function (result) {
                console.log(result);
            })
    }

    $scope.deleteAuction = function (id) {
        $http.post("/Admin/DeleteAuction", { id: id })
            .success(function (result) {
                $scope.auctionList = result;
            })
            .error(function (result) {
                console.log(result);
            })
    }

});


//Admin CRUD-Users
//////////////////////////////////////////////////////////////

app.controller("adminUsersCtrl", function ($scope, $http) {

    $scope.userList = [];
    $scope.adminUser = "";


    $http.get("/Admin/GetUsers")
            .success(function (result) {
                $scope.userList = result;
            })
            .error(function (result) {
                console.log(result);
            })

    $scope.createUser = function (newuser) {
        $http.post("/Admin/CreateUser", { newuser: newuser })
            .success(function (result) {
                $scope.userList = result;
                $scope.adminUser = "";
            })
            .error(function (result) {
                console.log(result);
            })
    }

    $scope.selectUserById = function (id) {
        $http.post("/Admin/SelectUserById", { id: id })
            .success(function (result) {
                $scope.adminUser = result;
            })
            .error(function (result) {
                console.log(result);
            })
    }

    $scope.updateUser = function (user) {
        $http.post("/Admin/UpdateUser", { user: user })
            .success(function (result) {
                $scope.userList = result;
                $scope.adminUser = "";
            })
            .error(function (result) {
                console.log(result);
            })
    }

    $scope.deleteUser = function (id) {
        $http.post("/Admin/DeleteUser", { id: id })
            .success(function (result) {
                $scope.userList = result;
            })
            .error(function (result) {
                console.log(result);
            })
    }

});


//Auction Details
//////////////////////////////////////////////////////////////
app.controller("auctionDetailsController", function ($scope, $http, $routeParams) {
    
    $scope.auctionDetails;

    $http.post("/Auctions/GetAuctionDetails", { id: $routeParams.id })
        .success(function (result) {
            $scope.auctionDetails = result;
        })
        .error(function (result) {
            console.log(result);
        })

});