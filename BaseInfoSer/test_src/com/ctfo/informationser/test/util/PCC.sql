
select v.vehicle_no,v.plate_color,t.oem_code,ss.commaddr from tb_vehicle v,tb_terminal t,tb_sim ss

where ss.sid = 
(
    select s.sid from tr_serviceunit s
    where s.vid=(
      select v.vid from tb_vehicle v
      where v.plate_color=1 and v.vehicle_no = '1111'
    )
) and t.tid= (
  select s.tid from tr_serviceunit s
    where s.vid=(
      select v.vid from tb_vehicle v
      where v.plate_color=1 and v.vehicle_no = '1111'
    )
)
 and ss.sid= (
  select s.sid from tr_serviceunit s
    where s.vid=(
      select v.vid from tb_vehicle v
      where v.plate_color=1 and v.vehicle_no = '1111'
    )
)
