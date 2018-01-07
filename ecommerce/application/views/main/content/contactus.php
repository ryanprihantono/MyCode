
<h1 class="head-mainbar round5">Contact Us</h1>
    <form class="uniform" style="padding-left: 30px;" action="" method="post" enctype="multipart/form-data">
        
        <div>
            <label class="uniform">Subject<font color="#FF0000">*</font></label>
            <input type="text" name="subject" id="subject" placeholder="Subject" class="uniform" style="width:200px;" />
            <div id="err_subject"></div>
            <div class="clear"></div>
        </div>
        <div>
            <label class="uniform">Email<font color="#FF0000">*</font></label>
            <?php if($this->session->userdata('email')==FALSE){ ?>
            <input type="text" name="email" id="email" placeholder="Email" class="uniform" style="width:200px;" />
            <?php }else{ ?>
            <label class="uniform" style="font-weight:bold"><?php echo $this->session->userdata('email'); ?></label>
            <?php } ?>
            <div id="err_email"></div>
            <div class="clear"></div>
        </div>
        
         <div style="height:auto;margin-bottom:-60px">
            <label class="uniform">Message<font color="#FF0000">*</font></label>
            <textarea name="message" id="message" placeholder="Type your message here" class="uniform" cols="50" rows="6"></textarea>
            <div id="err_message"></div>
            <div class="clear"></div>
        </div>
        <div>
            <button type="button" class="uniform" id="send">Send</button>
        </div>
    </form>
<div class="clear"></div>
<div style="text-align:right;border-top:#000 1px solid;margin-top:10px;padding-top:5px">
If you have questions about your order, please contact us at</p><br />
<p>
<?php 
	foreach($contactus->result() as $row){
		echo "$row->type : $row->contact | ";
	}
?>
</p>
</div>