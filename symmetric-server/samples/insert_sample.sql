------------------------------------------------------------------------------
-- Sample Data
------------------------------------------------------------------------------
delete from alarms;

-- alarms to sell and their prices
insert into alarms (alarm_id, name) values (11000002, 'Yummy Gum');

------------------------------------------------------------------------------
-- Clear and load SymmetricDS Configuration
------------------------------------------------------------------------------

delete from sym_trigger_router;
delete from sym_trigger;
delete from sym_router;
delete from sym_channel where channel_id in ('alarm');
delete from sym_node_group_link;
delete from sym_node_group;
delete from sym_node_host;
delete from sym_node_identity;
delete from sym_node_security;
delete from sym_node;

------------------------------------------------------------------------------
-- Channels
------------------------------------------------------------------------------

-- Channel "alarm" for tables related to alarms for purchase
insert into sym_channel 
(channel_id, processing_order, max_batch_size, enabled, description)
values('alarm', 1, 100000, 1, 'alarm data');

------------------------------------------------------------------------------
-- Node Groups
------------------------------------------------------------------------------

insert into sym_node_group (node_group_id) values ('pri');
insert into sym_node_group (node_group_id) values ('sec');

------------------------------------------------------------------------------
-- Node Group Links
------------------------------------------------------------------------------

-- Primary sends changes to Secondary when Secondary pulls from Primary
insert into sym_node_group_link (source_node_group_id, target_node_group_id, data_event_action) values ('pri', 'sec', 'W');

-- Secondary sends changes to Primary when Secondary pushes to Primary
insert into sym_node_group_link (source_node_group_id, target_node_group_id, data_event_action) values ('sec', 'pri', 'P');

------------------------------------------------------------------------------
-- Triggers
------------------------------------------------------------------------------

-- Triggers for tables on "alarm" channel
insert into sym_trigger (trigger_id,source_table_name, channel_id, last_update_time,create_time, sync_on_incoming_batch)
values('TriggerAll', '*', 'alarm', current_timestamp, current_timestamp,1);

------------------------------------------------------------------------------
-- Routers
------------------------------------------------------------------------------

-- Default router sends all data from Primary to Secondary 
insert into sym_router 
(router_id,source_node_group_id,target_node_group_id,router_type,create_time,last_update_time)
values('pri_2_sec', 'pri', 'sec', 'default',current_timestamp, current_timestamp);

-- Default router sends all data from Secondary to Primary
insert into sym_router 
(router_id,source_node_group_id,target_node_group_id,router_type,create_time,last_update_time)
values('sec_2_pri', 'sec', 'pri', 'default',current_timestamp, current_timestamp);

-- Column match router will subset data from Primary to specific Secondary
insert into sym_router 
(router_id,source_node_group_id,target_node_group_id,router_type,router_expression,create_time,last_update_time)
values('pri_2_one_sec', 'pri', 'sec', 'column','SEC_ID=:EXTERNAL_ID or OLD_SEC_ID=:EXTERNAL_ID',current_timestamp, current_timestamp);


------------------------------------------------------------------------------
-- Trigger Routers
------------------------------------------------------------------------------

-- Send all alarms to all Secondary nodes
insert into sym_trigger_router (trigger_id,router_id,initial_load_order,last_update_time,create_time)
values('TriggerAll','pri_2_sec', 100, current_timestamp,current_timestamp);

insert into sym_trigger_router (trigger_id,router_id,initial_load_order,last_update_time,create_time)
values('TriggerAll','sec_2_pri', 100, current_timestamp, current_timestamp); 