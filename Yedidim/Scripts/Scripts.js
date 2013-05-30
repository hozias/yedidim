var YedidimJS = new function () {
    this.Init = function () {
        var self = this;
        $('.icon-edit').click(function (e) {
            e.preventDefault();
            if($(this).hasClass('active')){
                self.SubmitForm($(this));
                $(this).removeClass('active');            
            }else {
                self.EnableFields(this);
                $(this).addClass('active');    
            }
        });

        $('.icon-cancel-edit').click(function () {
            self.DisableFields();
              $('.icon-edit').removeClass('active');
        });

        $('.greg-date').blur(function() {
            var dateString = $(this).val();
            //validate date string
            if (isNaN(Date.parse(dateString))) return false;

            var model = { "Date": dateString };
            self.Ajax('/Utils/GetHebDate', SetHebDate, model, $(this));
        });

        //add toggle click to headers
        $('.legend').toggle(
          function () {
              $(this).addClass('legend-active').next().slideDown('fast');
          },
          function () {
              $(this).removeClass('legend-active').next().slideUp('fast');
          }
        );

        $('#collapseAll').click(function (e) {
            e.preventDefault();
            self.SlideContainer(0);
        });

        $('#expandAll').click(function (e) {
            e.preventDefault();
            self.SlideContainer(1);
        });

        $('.icon-delete, .icon-delete-child').on('click', function (e) {
            var path, title, text = "";
            var $o = $(this);
            var userid = $o.attr('itemid');

            var isMainUser = $o.hasClass('icon-delete') ? true : false;
            
            if (isMainUser) {
                path = "../YadidMain/Delete/";
                title = "Delete?";
                text = "This will delete all user and all related data<br /><br /><label>All user data will be deleted: Members, Children and Events</label>"
            }
            else {
                path = "../Child/Delete/";
                title = "Delete?";
                text = "This will delete child data"
            }

            //$("#dialog-confirm")
            $("<div class='dialog' title='" + title + "'><p><span class='icon-alert'></span>" + text + "</p></div>")
                .dialog({
                resizable: false,
                draggable : false,
                height: 140,
                modal: true,
                buttons: {
                    "Delete": function () {
                        window.location.href = path + userid;
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                }
            });

        });
    }

    this.$d = $('.inputs');
    this.SlideContainer = function (dir) {
        var self = this;
        $.each(this.$d, function (i, elm) {
            if (dir > 0)
                $(elm).slideDown('fast').prev().addClass('legend-active');
            else {
                $(elm).slideUp('fast').prev().removeClass('legend-active');
                //self.DisableFields();
                //$('.icon-edit').removeClass('active');
            }
        });
    }

    this.EnableFields = function (elm) {
        var $container = $(elm).parent().parent();
        //$container.find('input').removeAttr('disabled');
        $('.inputs input').removeAttr('disabled');
    }

    this.DisableFields = function () {
        $('.inputs input').attr('disabled', 'disabled');
    }

    this.SubmitForm = function (jqObj) {
        //$('form').submit();
        //jqObj.closest("form").submit();
        $('.page-content').find('form').submit();
    }


    //Publish Ajax calls + return callback
    this.Ajax = function (path, callback, model, initiator) {
       
        $.ajax({
            type: 'POST',
            url: path,            
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(model),
            dataType: 'json',
            success: function (result) {
                callback(result, initiator);
            }
        });
    }

    function SetHebDate(data, initiator) {
        var elm = initiator.attr('related');
        $('#' + elm).val(data.HebDate);
        initiator.parent().next().find('.not-editable').text(data.HebDate);
    }
}



$(document).ready(function () {
    YedidimJS.Init();
});