SELECT log_id, "message-text", "interface-name", "user-name", "ip-address", "occurrence-date", "record-type", "exception-text", "exception-stack-trace"
FROM public."operation-log" order by 1 desc


--truncate table public."operation-log"
--ALTER SEQUENCE "public"."operation-log_log_id_seq" RESTART WITH 1;
