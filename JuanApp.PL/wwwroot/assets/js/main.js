document.addEventListener('DOMContentLoaded', function () {

    document.querySelectorAll('.btn-quick-view').forEach(function (btn) {
        btn.addEventListener('click', function (e) {
            e.preventDefault();

            const productId = this.dataset.id;

            fetch(`/Product/ProductModal?id=${productId}`)
                .then(response => {
                    if (!response.ok) throw new Error('Network response was not ok');
                    return response.text();
                })
                .then(html => {
                    const modalContainer = document.getElementById('quick_view');
                    modalContainer.querySelector('.modal-body').innerHTML = html;

                    const modal = new bootstrap.Modal(modalContainer);
                    modal.show();

                    const $largeSlider = $(modalContainer).find('.product-large-slider');
                    const $navSlider = $(modalContainer).find('.pro-nav');

                    if ($largeSlider.length && $navSlider.length) {
                        $largeSlider.slick({
                            slidesToShow: 1,
                            slidesToScroll: 1,
                            arrows: false,
                            fade: true,
                            asNavFor: $navSlider
                        });

                        $navSlider.slick({
                            slidesToShow: 4,
                            slidesToScroll: 1,
                            asNavFor: $largeSlider,
                            dots: false,
                            focusOnSelect: true,
                            arrows: false
                        });
                    }

                    $(modalContainer).find('.pro-qty').each(function () {
                        const $this = $(this);
                        if ($this.find('.qtybtn').length === 0) {
                            $this.prepend('<span class="dec qtybtn">-</span>');
                            $this.append('<span class="inc qtybtn">+</span>');
                        }

                        $this.off('click').on('click', '.qtybtn', function () {
                            let oldValue = parseInt($this.find('input').val());
                            let newVal;
                            if ($(this).hasClass('inc')) {
                                newVal = oldValue + 1;
                            } else {
                                newVal = oldValue - 1;
                                if (newVal < 1) newVal = 1;
                            }
                            $this.find('input').val(newVal);
                        });
                    });
                })
                .catch(error => {
                    console.error('Fetch error:', error);
                    alert('Failed to load product details.');
                });
        });
    });

});
