//app.js

var app = angular.module("app", ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider.
           
        when('/index', {
            templateUrl: 'angular/views/index.html'
        }).
        when('/about', {
            templateUrl: 'angular/views/about.html'
        }).
        when('/contact', {
            templateUrl: 'angular/views/contact.html'
        })
        .when('/auctions', {
            templateUrl: 'angular/views/auctions.html'
        })
})