#!/usr/bin/python

import csv
import getopt
import json
import sys

try:
    from urllib.request import Request, urlopen  # Python 3
except ImportError:
    from urllib2 import Request, urlopen  # Python 2


def usage():
    print()
    print("Get observations from the NVE Hydrological API (HydAPI)")
    print("Parameters:")
    print("   -s: StationId (mandatory)")
    print("   -p: Parameter (mandatory)")
    print("   -r: Resolution time. 0 (instantenous),60 (hourly) or 1440 (daily). (mandatory)")
    print("   -f: From time. Given on format \"YYYY-MM-DD\" or \"YYYY-MM-DDThh:mm:ss\" (optional)")
    print("   -t: To time. Given on format \"YYYY-MM-DD\" or \"YYYY-MM-DDThh:mm:ss\" (optional)")
    print("   -h: This help")
    print()
    print("Example:")
    print("    python get-observations.py -a \"INSERT_APIKEY_HERE\" -s 6.10.0 -p 1000 -r 60 -f \"2019-01-01T10:00:00\" -t \"2019-01-01T16:00:00")
    print()

def main(argv):
    try:
        opts, args = getopt.getopt(argv, "a:s:p:r:hf:t:")
    except getopt.GetoptError as err:
        print(str(err))  # will print something like "option -a not recognized"
        usage()
        sys.exit(2)

    station = None
    parameter = None
    resolution_time = None
    api_key = None
    from_time = None
    to_time = None
     
    for opt, arg in opts:
        if opt == "-s":
            station = arg
        elif opt == "-p":
            parameter = arg
        elif opt == "-r":
            resolution_time = arg
        elif opt == "-a":
            api_key = arg
        elif opt == "-f":
            from_time = arg
        elif opt == "-t":
            to_time = arg
        elif opt == "-h":
            usage()
            sys.exit()
        else:
            assert False, "unhandled option"

    if api_key == None:
        print("Error: You must supply the api-key with your request (-a)")
        usage()
        sys.exit(2)

    if station == None or parameter == None  or resolution_time == None:
        print("Error: You must supply the parameters station (-s), parameter (-p) and resolution time (-r)")
        usage()
        sys.exit(2)

    baseurl = "https://hydapi.nve.no/api/v0.9/Observations?StationId={station}&Parameter={parameter}&ResolutionTime={resolution_time}"
    
    url = baseurl.format(station=station, parameter=parameter, resolution_time=resolution_time)

    if from_time is not None: 
        url = "{url}&From={from_time}".format(url=url, from_time=from_time)

    if to_time is not None:
        url = "{url}&To={to_time}".format(url=url, to_time=to_time)

    print(url)
    
    request_headers = {
        "Accept": "application/json",
        "X-API-Key": api_key
    }

    request = Request(url, headers=request_headers)

    response = urlopen(request)
    content = response.read()
    parsed_result = json.loads(content)

    for observation in parsed_result["data"]:
        print(observation)

if __name__ == "__main__":
   main(sys.argv[1:])