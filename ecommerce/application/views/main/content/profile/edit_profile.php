<form class="uniform" style="padding-left: 30px;" action="" method="post" enctype="multipart/form-data">
        
        <div>
            <label class="uniform">Username</label>
            <label class="uniform" style="font-weight:bold"><?php echo $user->username; ?></label>
            <div class="clear"></div>
        </div>
        <div>
            <label class="uniform">Email</label>
            <input type="text" id="email" class="uniform" value="<?php echo $user->email; ?>" style="margin-right:5px" />
            <a href="#" id="change_email"><img src='<?php echo base_url(); ?>assets/admin/images/icon/save.png' title='Save Change' witdh='15' height='15'/></a>
            <div class="clear"></div>
        </div>
       
        <div style="height:auto;margin-bottom:-30px">
            <label class="uniform">Password</label>
            <label class="uniform" id="password" style="font-weight:bold">Hidden<a href="#" id="edit_password"><img src='<?php echo base_url(); ?>assets/admin/images/icon/edit.png' title='Change Password' witdh='15' height='15' style="margin-left:5px"/></a></label>
            <div class="clear"></div>
        </div>
        <div style="height:auto;margin-bottom:-20px;margin-top:auto">
            <label class="uniform">Address</label>
            <textarea class="uniform" id="address" style="margin-right:5px" ><?php echo $user->address; ?></textarea>
            <a href="#" id="change_address"><img src='<?php echo base_url(); ?>assets/admin/images/icon/save.png' title='Save Change' witdh='15' height='15'/></a>
            <div class="clear"></div>
        </div>
         <div style="margin-top:auto">
            <label class="uniform">City</label>
            <input type="text" class="uniform" id="city" style="margin-right:5px" value="<?php echo $user->city; ?>" />
            <a href="#" id="change_city"><img src='<?php echo base_url(); ?>assets/admin/images/icon/save.png' title='Save Change' witdh='15' height='15'/></a>
            <div class="clear"></div>
        </div>
         <div>
            <label class="uniform">Phone</label>
            <input type="text" class="uniform" id="phone" style="margin-right:5px" value="<?php echo $user->phone; ?>" />
            <a href="#" id="change_phone"><img src='<?php echo base_url(); ?>assets/admin/images/icon/save.png' title='Save Change' witdh='15' height='15'/></a>
            <div class="clear"></div>
        </div>
        <div>
            <label class="uniform">I am</label>
            <label class="uniform" style="margin-right:100px" >
            <select name="gender" class="uniform">
                <option value="Male">Male</option>
                <?php if($user->gender=="Female"){?>
                <option value="Female" selected="selected">Female</option>
                <?php }else{ ?>
                <option value="Female">Female</option>
                <?php } ?>
            </select></label><a href="#" id="change_gender"><img src='<?php echo base_url(); ?>assets/admin/images/icon/save.png' title='Save Change' witdh='15' height='15'/>
            </a>
            <div class="clear"></div>
        </div>
        <div>
            <label class="uniform">Birthday</label>
            <input type="text" name="birthday" id="birthday" placeholder="Birthday" class="uniform datepicker" value="<?php echo $user->birthday; ?>" style="margin-right:5px" />
            <a href="#" id="change_birthday"><img src='<?php echo base_url(); ?>assets/admin/images/icon/save.png' title='Save Change' witdh='15' height='15'/></a>
        </div>
        <div>
            <button type="button" class="uniform" id="register">Register</button>
        </div>
    </form>