<div class="sidebar-box">
    <h1 class="sidebar-head round5-top">News</h1>
    
    <div class="sidebar-cycle">
        <!-- looping -->
       	<?php
			foreach($news->result() as $row){
        ?>
        <div class="sidebar-content">  
            <div class="sidebar-img-box">
                <a href="<?php echo base_url()."main/news/".$row->news_id;?>"><img width="194" src="<?php echo base_url(); ?>assets/uploads/news_images/<?php echo $row->picture; ?>"/></a>
            </div>
            <p class="sidebar-posttime">
                <?php echo $row->date; ?>
            </p>
            <h1 class="sidebar-subhead"><?php echo $row->title; ?></h1>
            <div class="sidebar-desc"><?php echo $row->news; ?></div>
            <p class="readmore">
                <a href="<?php echo base_url()."main/news/".$row->news_id;?>">Read More</a>
            </p>
        </div>
        <?php
			}
        ?>
        
    </div>
</div>