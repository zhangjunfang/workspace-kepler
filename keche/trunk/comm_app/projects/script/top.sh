#!/bin/sh
top -p$(ps -ef | grep -E '(msg|was|pcc|filesrv|nodesrv|authsrv|pushsrv|filemgr)' |  awk 'BEGIN{ORS=","}{print $2}' | sed 's/.$//')