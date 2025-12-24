// ==========================================================================
// Juan E-commerce Custom JavaScript
// ==========================================================================

(function($) {
    'use strict';

    // Toastr configuration
    if (typeof toastr !== 'undefined') {
        toastr.options = {
            "closeButton": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "timeOut": "3000",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    }

    // Update cart count in header
    window.updateCartCount = function(count) {
        if (count !== undefined) {
            $('.cart-count, #cart-count').text(count);
        } else {
            loadCartCount();
        }
    };

    // Load cart count on page load
    function loadCartCount() {
        $.ajax({
            url: '/Basket/GetItemCount',
            type: 'GET',
            success: function(response) {
                if (response.itemCount !== undefined) {
                    $('.cart-count, #cart-count').text(response.itemCount);
                }
            },
            error: function(xhr, status, error) {
                console.error('Failed to load cart count:', error);
            }
        });
    }

    // Subscribe function
    window.subscribe = function(event) {
        event.preventDefault();
        const email = document.getElementById('subscribeEmail').value;

        if (!email) {
            toastr.warning('Please enter your email');
            return;
        }

        fetch('/Subscriber/Subscribe', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(email)
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                toastr.success(data.message);
                document.getElementById('subscribeEmail').value = '';
            } else {
                toastr.error(data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            toastr.error('An error occurred. Please try again.');
        });
    };

    // Initialize when document is ready
    $(document).ready(function() {
        // Load cart count
        loadCartCount();

        // Mini cart hover functionality
        initMiniCart();

        // Off-canvas search
        initOffcanvasSearch();

        // Product quick view
        initQuickView();

        // Add to cart functionality
        initAddToCart();
    });

    // Mini cart functionality
    function initMiniCart() {
        let miniCartTimeout;
        const $minicartBtn = $('.minicart-btn');
        
        // Create mini cart dropdown if not exists
        if ($('.mini-cart-dropdown').length === 0) {
            $minicartBtn.parent().css('position', 'relative');
            $minicartBtn.after(`
                <div class="mini-cart-dropdown">
                    <div class="mini-cart-content">
                        <div class="mini-cart-loading">
                            <i class="fa fa-spinner fa-spin fa-2x"></i>
                        </div>
                    </div>
                </div>
            `);
        }

        // Show mini cart on hover
        $minicartBtn.on('mouseenter', function() {
            clearTimeout(miniCartTimeout);
            loadMiniCart();
            $('.mini-cart-dropdown').fadeIn(200);
        });

        // Hide mini cart when mouse leaves
        $minicartBtn.parent().on('mouseleave', function() {
            miniCartTimeout = setTimeout(function() {
                $('.mini-cart-dropdown').fadeOut(200);
            }, 300);
        });

        $('.mini-cart-dropdown').on('mouseenter', function() {
            clearTimeout(miniCartTimeout);
        });

        $('.mini-cart-dropdown').on('mouseleave', function() {
            miniCartTimeout = setTimeout(function() {
                $('.mini-cart-dropdown').fadeOut(200);
            }, 300);
        });
    }

    // Load mini cart content
    function loadMiniCart() {
        // Check if user is authenticated by checking if cart icon has data
        $.ajax({
            url: '/Basket/GetBasketItems',
            type: 'GET',
            success: function(response) {
                if (response.success && response.items) {
                    let html = '';
                    if (response.items.length > 0) {
                        html += '<div style="max-height: 300px; overflow-y: auto;">';
                        let total = 0;
                        response.items.forEach(function(item) {
                            const itemTotal = item.price * item.quantity;
                            total += itemTotal;
                            html += `
                                <div class="mini-cart-item">
                                    <img src="/${item.imageUrl}" alt="${item.productName}">
                                    <div style="flex: 1;">
                                        <h6>${item.productName}</h6>
                                        <p>Qty: ${item.quantity} × ₼${item.price.toFixed(2)}</p>
                                    </div>
                                </div>
                            `;
                        });
                        html += '</div>';
                        html += `
                            <div class="mini-cart-total">
                                <div class="total-row">
                                    <strong>Total:</strong>
                                    <strong>₼${total.toFixed(2)}</strong>
                                </div>
                                <a href="/Basket" class="btn btn-default" style="width: 100%; text-align: center; display: block; margin-top: 10px; padding: 10px;">View Cart</a>
                            </div>
                        `;
                    } else {
                        html = '<p style="text-align: center; padding: 20px; color: #666;">Your cart is empty</p>';
                    }
                    $('.mini-cart-content').html(html);
                } else if (response.requiresLogin) {
                    $('.mini-cart-content').html('<p style="text-align: center; padding: 20px; color: #666;">Please login to view cart</p>');
                }
            },
            error: function(xhr) {
                if (xhr.status === 401) {
                    $('.mini-cart-content').html('<p style="text-align: center; padding: 20px; color: #666;">Please login to view cart</p>');
                } else {
                    $('.mini-cart-content').html('<p style="text-align: center; color: #999;">Failed to load cart</p>');
                }
            }
        });
    }

    // Off-canvas search functionality
    function initOffcanvasSearch() {
        $('.offcanvas-btn').on('click', function(e) {
            e.preventDefault();
            $('.off-canvas-wrapper').addClass('show');
            $('body').addClass('fix');
        });

        $('.btn-close-off-canvas, .off-canvas-overlay').on('click', function() {
            $('.off-canvas-wrapper').removeClass('show');
            $('body').removeClass('fix');
        });

        // Search form submit
        $('.search-box-offcanvas form').on('submit', function(e) {
            e.preventDefault();
            const searchTerm = $(this).find('input').val();
            if (searchTerm) {
                window.location.href = '/Shop?search=' + encodeURIComponent(searchTerm);
            }
        });
    }

    // Product quick view
    function initQuickView() {
        $(document).on('click', '.quick-view-btn', function(e) {
            e.preventDefault();
            const productId = $(this).data('product-id');
            
            // Load quick view modal
            $.ajax({
                url: '/Product/QuickView/' + productId,
                type: 'GET',
                success: function(response) {
                    // Show modal with product details
                    if (response.success) {
                        showQuickViewModal(response.product);
                    }
                },
                error: function() {
                    toastr.error('Failed to load product details');
                }
            });
        });
    }

    // Show quick view modal
    function showQuickViewModal(product) {
        // Create modal HTML and show it
        // This would require a modal template
        console.log('Quick view:', product);
    }

    // Add to cart functionality
    function initAddToCart() {
        $(document).on('click', '.add-to-cart-btn', function(e) {
            e.preventDefault();
            const $btn = $(this);
            const productId = $btn.data('product-id');
            const colorId = $btn.data('color-id') || $('.color-option.active').data('color-id');
            const sizeId = $btn.data('size-id') || $('.size-option.active').data('size-id');
            const quantity = parseInt($btn.data('quantity') || $('.quantity-input').val() || 1);

            if (!productId) {
                toastr.error('Product not found');
                return;
            }

            // Disable button and show loading
            const originalText = $btn.html();
            $btn.prop('disabled', true).html('<i class="fa fa-spinner fa-spin"></i> Adding...');

            $.ajax({
                url: '/Basket/AddToBasket',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({
                    productId: productId,
                    colorId: colorId,
                    sizeId: sizeId,
                    quantity: quantity
                }),
                success: function(response) {
                    if (response.success) {
                        toastr.success('Product added to cart');
                        updateCartCount();
                    } else {
                        toastr.error(response.message || 'Failed to add to cart');
                    }
                },
                error: function(xhr) {
                    let errorMessage = 'Failed to add to cart';
                    if (xhr.responseJSON && xhr.responseJSON.message) {
                        errorMessage = xhr.responseJSON.message;
                    }
                    toastr.error(errorMessage);
                },
                complete: function() {
                    // Re-enable button
                    $btn.prop('disabled', false).html(originalText);
                }
            });
        });

        // Color selector
        $(document).on('click', '.color-option', function() {
            $('.color-option').removeClass('active');
            $(this).addClass('active');
        });

        // Size selector
        $(document).on('click', '.size-option', function() {
            $('.size-option').removeClass('active');
            $(this).addClass('active');
        });
    }

})(jQuery);

