# Python example

Below are some examples for using Python to fetch data from the Hydrological API (HydAPI).

## Setup

You may need to install Python which is available on many platforms. See [Python homepage](https://www.python.org/).

## Generate API-key

If you haven't generated an API-key yet, you can do that on this page:

https://hydapi.nve.no/users

In the examples below, substitute the text INSERT_KEY_HERE with the API-key you obtained.

## Getting observations

Getting the latest observed value for a time series:

```
python get-observations.py -a "INSERT_KEY_HERE" -s 12.209.0 -p 1000 -r 0
```

Getting all hourly observations between 2018-01-01 10:00:00 and 2018-01-14 20:00:00 (all timestamps are in UTC-0):

```
python get-observations.py -a "INSERT_KEY_HERE" -s 12.209.0 -p 1000 -r 60 -t "2018-01-01T10:00:00/2018-01-14T20:00:00"
```
