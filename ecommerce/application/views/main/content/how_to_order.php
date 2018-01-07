<h1 class="head-mainbar round5">How to Order</h1>
<?php
	$i=1; 
	foreach($how_to_order->result() as $row){ 
?>
	<div style="margin:10px 0 10px 50px">
        <div style="float:left;font-size:20px;margin-right:18px"><?php echo $i; ?></div>
        <div style="font-size:18px"><?php echo $row->content; ?></div>
    </div>      
<?php 
		$i++;
	} 
?>
<div class="clear"></div>
<h1 class="head-mainbar round5">Term of service</h1>
<?php
	$i=1; 
	foreach($term_of_service->result() as $row){ 
?>
    <div style="margin:10px 0 10px 50px">
        <div style="float:left;font-size:18px;margin-right:20px"><?php echo $i; ?></div>
        <div style="font-size:18px"><?php echo $row->content; ?></div>
    </div>            
<?php 
		$i++;
	} 
?>
<div class="clear"></div>