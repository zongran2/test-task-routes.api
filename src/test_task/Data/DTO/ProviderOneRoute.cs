﻿namespace TestTask.Data.DTO;

public class ProviderOneRoute
{
    // Mandatory
    // Start point of route
    public string From { get; set; }

    // Mandatory
    // End point of route
    public string To { get; set; }

    // Mandatory
    // Start date of route
    public DateTime DateFrom { get; set; }

    // Mandatory
    // End date of route
    public DateTime DateTo { get; set; }

    // Mandatory
    // Price of route
    public decimal Price { get; set; }

    // Mandatory
    // Timelimit. After it expires, route became not actual
    public DateTime TimeLimit { get; set; }
}

// HTTP GET http://provider-one/api/v1/ping
//      - HTTP 200 if provider is ready
//      - HTTP 500 if provider is down