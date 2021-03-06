var ComingSoon = function () {

    return {

        home: function () {
            location.replace('/home/index');
        },

        back: function () {
            history.back();
        },

        //main function to initiate the module
        init: function () {
            var austDay = new Date();
            austDay = new Date(austDay.getFullYear() + 1, 1 - 1, 26);
            $('#defaultCountdown').countdown({ until: austDay });
            $('#year').text(austDay.getFullYear());
            $('.btnBack').click(this.back);
            $('.btnHome').click(this.home);

            $.backstretch([
		        '/Content/Metronic4.5.6/pages/media/bg/1.jpg',
		        '/Content/Metronic4.5.6/pages/media/bg/2.jpg',
		        '/Content/Metronic4.5.6/pages/media/bg/3.jpg',
		        '/Content/Metronic4.5.6/pages/media/bg/4.jpg'
            ], {
                fade: 1000,
                duration: 10000
            });
        }

    };

}();

jQuery(document).ready(function () {
    ComingSoon.init();
});