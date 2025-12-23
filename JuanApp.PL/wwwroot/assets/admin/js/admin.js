N// ==========================================================================
// Admin Panel JavaScript Functions
// ==========================================================================

(function() {
    'use strict';

    // Initialize when DOM is ready
    document.addEventListener('DOMContentLoaded', function() {
        initSidebar();
        initTooltips();
        initPopovers();
        initDataTables();
        initImagePreviews();
        initConfirmDialogs();
        initFormValidation();
        initAutoHideAlerts();
    });

    // Sidebar Toggle Functionality
    function initSidebar() {
        const sidebar = document.getElementById('sidebar');
        const toggleBtn = document.getElementById('toggleSidebar');
        
        if (toggleBtn && sidebar) {
            toggleBtn.addEventListener('click', function() {
                sidebar.classList.toggle('collapsed');
                localStorage.setItem('sidebarCollapsed', sidebar.classList.contains('collapsed'));
            });

            // Load saved state
            if (localStorage.getItem('sidebarCollapsed') === 'true') {
                sidebar.classList.add('collapsed');
            }

            // Mobile responsive
            if (window.innerWidth <= 768) {
                sidebar.classList.add('collapsed');
            }
        }
    }

    // Initialize Bootstrap Tooltips
    function initTooltips() {
        const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.map(function(tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    }

    // Initialize Bootstrap Popovers
    function initPopovers() {
        const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
        popoverTriggerList.map(function(popoverTriggerEl) {
            return new bootstrap.Popover(popoverTriggerEl);
        });
    }

    // Initialize DataTables
    function initDataTables() {
        if (typeof $.fn.DataTable !== 'undefined') {
            $('.data-table').DataTable({
                language: {
                    search: "Axtar:",
                    lengthMenu: "_MENU_ sətir göstər",
                    info: "_START_-dən _END_-ə qədər, cəmi _TOTAL_ nəticə",
                    infoEmpty: "Nəticə tapılmadı",
                    infoFiltered: "(cəmi _MAX_ nəticədən süzülüb)",
                    paginate: {
                        first: "İlk",
                        last: "Son",
                        next: "Növbəti",
                        previous: "Əvvəlki"
                    },
                    emptyTable: "Cədvəldə məlumat yoxdur"
                },
                pageLength: 10,
                responsive: true,
                order: [[0, 'desc']]
            });
        }
    }

    // Image Preview Functionality
    function initImagePreviews() {
        const imageInputs = document.querySelectorAll('input[type="file"][accept*="image"]');
        
        imageInputs.forEach(function(input) {
            input.addEventListener('change', function(e) {
                const files = e.target.files;
                const previewContainer = document.getElementById('imagePreview');
                
                if (previewContainer) {
                    previewContainer.innerHTML = '';
                    
                    Array.from(files).forEach(function(file) {
                        if (file.type.startsWith('image/')) {
                            const reader = new FileReader();
                            
                            reader.onload = function(e) {
                                const div = document.createElement('div');
                                div.className = 'image-preview-item';
                                div.innerHTML = `
                                    <img src="${e.target.result}" alt="Preview">
                                    <button type="button" class="remove-image" onclick="this.parentElement.remove()">
                                        <i class="bi bi-x"></i>
                                    </button>
                                `;
                                previewContainer.appendChild(div);
                            };
                            
                            reader.readAsDataURL(file);
                        }
                    });
                }
            });
        });
    }

    // Confirm Delete Dialogs with SweetAlert2
    function initConfirmDialogs() {
        // Delete forms with SweetAlert2
        document.addEventListener('submit', function(e) {
            const form = e.target;
            if (form.hasAttribute('data-confirm')) {
                e.preventDefault();
                const message = form.getAttribute('data-confirm') || 'Are you sure you want to delete this?';
                
                Swal.fire({
                    title: 'Are you sure?',
                    text: message,
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#ef4444',
                    cancelButtonColor: '#64748b',
                    confirmButtonText: 'Yes, delete it!',
                    cancelButtonText: 'Cancel'
                }).then((result) => {
                    if (result.isConfirmed) {
                        form.submit();
                    }
                });
            }
        });

        // Delete buttons with SweetAlert2
        const deleteButtons = document.querySelectorAll('[data-delete-url]');
        deleteButtons.forEach(function(btn) {
            btn.addEventListener('click', function(e) {
                e.preventDefault();
                const url = btn.getAttribute('data-delete-url');
                const message = btn.getAttribute('data-confirm') || 'Are you sure you want to delete this item?';
                
                Swal.fire({
                    title: 'Are you sure?',
                    text: message,
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#ef4444',
                    cancelButtonColor: '#64748b',
                    confirmButtonText: 'Yes, delete it!',
                    cancelButtonText: 'Cancel'
                }).then((result) => {
                    if (result.isConfirmed) {
                        deleteItem(url);
                    }
                });
            });
        });
    }

    // Form Validation
    function initFormValidation() {
        const forms = document.querySelectorAll('.needs-validation');
        
        Array.from(forms).forEach(function(form) {
            form.addEventListener('submit', function(event) {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }

    // Auto-hide Alerts
    function initAutoHideAlerts() {
        const alerts = document.querySelectorAll('.alert-auto-hide');
        
        alerts.forEach(function(alert) {
            setTimeout(function() {
                const bsAlert = new bootstrap.Alert(alert);
                bsAlert.close();
            }, 5000);
        });
    }

    // Show Loading Spinner
    window.showSpinner = function() {
        const spinner = document.createElement('div');
        spinner.className = 'spinner-overlay';
        spinner.id = 'loadingSpinner';
        spinner.innerHTML = `
            <div class="spinner-border spinner-border-custom" role="status">
                <span class="visually-hidden">Yüklənir...</span>
            </div>
        `;
        document.body.appendChild(spinner);
    };

    // Hide Loading Spinner
    window.hideSpinner = function() {
        const spinner = document.getElementById('loadingSpinner');
        if (spinner) {
            spinner.remove();
        }
    };

    // Delete Item via AJAX
    function deleteItem(url) {
        showSpinner();
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-Requested-With': 'XMLHttpRequest'
            }
        })
        .then(response => response.json())
        .then(data => {
            hideSpinner();
            if (data.success) {
                Swal.fire({
                    title: 'Deleted!',
                    text: 'Item has been deleted successfully.',
                    icon: 'success',
                    timer: 1500,
                    showConfirmButton: false
                }).then(() => {
                    location.reload();
                });
            } else {
                Swal.fire({
                    title: 'Error!',
                    text: data.message || 'An error occurred while deleting.',
                    icon: 'error'
                });
            }
        })
        .catch(error => {
            hideSpinner();
            Swal.fire({
                title: 'Error!',
                text: 'An error occurred while deleting.',
                icon: 'error'
            });
            console.error('Error:', error);
        });
    }

    // Show Alert Message
    window.showAlert = function(type, message) {
        const alertDiv = document.createElement('div');
        alertDiv.className = `alert alert-${type} alert-dismissible fade show position-fixed`;
        alertDiv.style.cssText = 'top: 20px; right: 20px; z-index: 9999; min-width: 300px;';
        alertDiv.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;
        document.body.appendChild(alertDiv);
        
        setTimeout(() => {
            const bsAlert = new bootstrap.Alert(alertDiv);
            bsAlert.close();
        }, 5000);
    };

    // Color Picker Preview
    window.updateColorPreview = function(input) {
        const preview = document.getElementById('colorPreview');
        if (preview) {
            preview.style.backgroundColor = input.value;
        }
    };

    // File Name Display
    window.updateFileName = function(input) {
        const fileNameDisplay = document.getElementById('fileName');
        if (fileNameDisplay && input.files.length > 0) {
            const fileNames = Array.from(input.files).map(f => f.name).join(', ');
            fileNameDisplay.textContent = fileNames;
        }
    };

    // Toggle Product Stock Status
    window.toggleStock = function(productId, checkbox) {
        const inStock = checkbox.checked;
        
        fetch(`/Admin/Product/ToggleStock/${productId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ inStock })
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                showAlert('success', 'Status yeniləndi!');
            } else {
                checkbox.checked = !inStock;
                showAlert('error', 'Xəta baş verdi!');
            }
        })
        .catch(error => {
            checkbox.checked = !inStock;
            showAlert('error', 'Xəta baş verdi!');
        });
    };

    // Bulk Actions
    window.executeBulkAction = function() {
        const action = document.getElementById('bulkAction').value;
        const checkboxes = document.querySelectorAll('.item-checkbox:checked');
        
        if (checkboxes.length === 0) {
            showAlert('warning', 'Zəhmət olmasa element seçin!');
            return;
        }

        const ids = Array.from(checkboxes).map(cb => cb.value);
        
        if (confirm(`Seçilmiş ${ids.length} element üzərində əməliyyat yerinə yetirmək istəyirsiniz?`)) {
            showSpinner();
            
            fetch('/Admin/BulkAction', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ action, ids })
            })
            .then(response => response.json())
            .then(data => {
                hideSpinner();
                if (data.success) {
                    showAlert('success', 'Əməliyyat uğurla yerinə yetirildi!');
                    setTimeout(() => location.reload(), 1000);
                } else {
                    showAlert('error', data.message);
                }
            })
            .catch(error => {
                hideSpinner();
                showAlert('error', 'Xəta baş verdi!');
            });
        }
    };

    // Select All Checkboxes
    window.toggleSelectAll = function(checkbox) {
        const checkboxes = document.querySelectorAll('.item-checkbox');
        checkboxes.forEach(cb => cb.checked = checkbox.checked);
    };

})();

