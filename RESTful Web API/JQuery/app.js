$(document).ready(
    () => {
        $('*').css('font-size', '25px')
        $('body').css('background-color', 'yellow')
        $('label').css('border', '2px solid black')


        $('input').focus(() => {
            $(this).css('background-color', 'red')
        })
        $('input').blur(() => {
            $(this).css('background-color', 'blue')
        })

        $('button').on(
            {
                click: () => {
                    $(this).css('background-color', 'red')
                    alert('Hello')
                }
            }
        )

        // $('label').on(
        //     {
        //         mouseenter: () => {
        //             console.log('ok');

        //             $(this).css('background-color', 'green')
        //         },
        //         mouseleave: () => {
        //             $(this).css('background-color', 'white')
        //         }
        //     }
        // )
        $('label').on(
            {
                mouseenter: function () {
                    console.log('ok');

                    $(this).css('background-color', 'green')
                },
                mouseleave: function () {
                    $(this).css('background-color', 'white')
                },
                dblclick: function () {
                    $(this).css('background-color', 'pink')
                },
                contextmenu: function () {
                    $(this).css('background-color', 'orange')
                }

            }
        )
    }
)