<h1 class="head-mainbar margin-top-20 round5"><?php echo $news_detail->title; ?></h1>
<div style="padding-left: 30px;padding-top:20px;padding-right:20px">
	<div style="font-size:18px"><?php echo date('d M Y',strtotime($news_detail->date)); ?></div>
	<div style="float:left;margin-top:20px;width:400px;margin-right:10px"><img width="400" src="<?php echo base_url(); ?>assets/uploads/news_images/<?php echo $news_detail->picture; ?>"/></div>
    <div style="margin-top:20px"><?php echo $news_detail->news; ?></div>
</div>