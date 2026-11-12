
const spaContainer = document.getElementById("spa-container");


//  اشخاص (Persons)

// واکشی و نمایش لیست اشخاص
async function loadPersonsView() {
    spaContainer.innerHTML = '<div class="text-center py-5"><div class="spinner-border text-primary" role="status"></div><p class="mt-2">در حال بارگذاری اشخاص...</p></div>';
    try {
        let response = await fetch('./api/Person'); 
        let persons = await response.json();

        let rowsHtml = '';
        if (persons.length === 0) {
            rowsHtml = '<tr><td colspan="4" class="text-center text-muted">هیچ شخصی ثبت نشده است.</td></tr>';

        } else {
            persons.forEach(p => {
                rowsHtml += `
                    <tr>
                      
                        <td>${p.nationalId}</td>  
                        <td>${p.lastName}</td>  
                        <td>${p.firstName}</td>
                        <td>${p.id}</td>
                        <td>
                            <button class="btn btn-sm btn-outline-warning me-1" onclick="loadEditPersonView(${p.id})">ویرایش</button>
                            <button class="btn btn-sm btn-outline-danger" onclick="deletePerson(${p.id})">حذف</button>
                        </td>
                    </tr>
                `;
            })
        }
        spaContainer.innerHTML = `
            <div class="card shadow-sm">
                <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                    <h5 class="mb-0">لیست اشخاص</h5>
                    <button class="btn btn-light btn-sm text-primary fw-bold" onclick="loadCreatePersonView()">+ ثبت شخص جدید</button>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-hover align-middle">
                            <thead class="table-light">
                                <tr>
                                <th>کد ملی</th>
                                <th>نام خانوادگی</th>
                                <th>نام</th>
                                    <th>شناسه</th>
                                    
                                    
                                    <th style="width: 160px;">عملیات</th>
                                </tr>
                            </thead>
                            <tbody>
                                ${rowsHtml}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        `;

    }catch(err) {
        spaContainer.innerHTML = `<div class="alert alert-danger">خطا در بارگذاری اطلاعات اشخاص: ${err.message}</div>`;

    }
} 
//  فرم افزودن شخص جدید
function loadCreatePersonView() {
    spaContainer.innerHTML = `
        <div class="card shadow-sm mx-auto" style="max-width: 500px;">
            <div class="card-header bg-success text-white">
                <h5 class="mb-0">ثبت شخص جدید</h5>
            </div>
            <div class="card-body">
                <form id="createPersonForm" onsubmit="handleCreatePerson(event)">
                    <div class="mb-3">
                        <label class="form-label">نام</label>
                        <input type="text" id="personName" class="form-control" required placeholder="مثال: رضا" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">نام خانوادگی</label>
                        <input type="text" id="personFamily" class="form-control" required placeholder="مثال: رضایی" />
                    </div>
                      <div class="mb-3">
                        <label class="form-label">کد ملی</label>
                        <input type="text" id="personNationalId" class="form-control" required placeholder="مثال: 4120358976" />
                    </div>
                    <div class="d-flex justify-content-between">
                        <button type="button" class="btn btn-secondary" onclick="loadPersonsView()">بازگشت</button>
                        <button type="submit" class="btn btn-success">ثبت نهایی</button>
                    </div>
                </form>
            </div>
        </div>
    `;
}

// ارسال داده فرم شخص جدید به API
async function handleCreatePerson(event) {
    event.preventDefault();
    const name = document.getElementById("personName").value;
    const family = document.getElementById("personFamily").value;
    const nationalId = document.getElementById("personNationalId").value;

    const response = await fetch('/api/Person', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ firstName: name, lastName: family, nationalId:nationalId })
    });

    if (response.ok) {
        loadPersonsView();
    } else {
        alert("خطا در ثبت شخص!");
    }
}

//  فرم ویرایش شخص
async function loadEditPersonView(id) {
    spaContainer.innerHTML = '<div class="text-center py-5"><div class="spinner-border text-warning" role="status"></div></div>';

    try {
        const response = await fetch(`/api/Person/${id}`);
        const person = await response.json();

        spaContainer.innerHTML = `
            <div class="card shadow-sm mx-auto" style="max-width: 500px;">
                <div class="card-header bg-warning text-dark">
                    <h5 class="mb-0">ویرایش شخص</h5>
                </div>
                <div class="card-body">
                    <form id="editPersonForm" onsubmit="handleEditPerson(event, ${person.id})">
                        <div class="mb-3">
                            <label class="form-label">نام</label>
                            <input type="text" id="editPersonName" class="form-control" value="${person.firstName}" required />
                        </div>
                        <div class="mb-3">
                            <label class="form-label">نام خانوادگی</label>
                            <input type="text" id="editPersonFamily" class="form-control" value="${person.lastName}" required />
                        </div>
                          <div class="mb-3">
                            <label class="form-label">کد ملی</label>
                            <input type="text" id="editpersonNationalId" class="form-control" value="${person.nationalId}" required />
                        </div>
                        <div class="d-flex justify-content-between">
                            <button type="button" class="btn btn-secondary" onclick="loadPersonsView()">انصراف</button>
                            <button type="submit" class="btn btn-warning">ذخیره تغییرات</button>
                        </div>
                    </form>
                </div>
            </div>
        `;
    } catch (err) {
        alert("خطا در بارگذاری مشخصات شخص!");
        loadPersonsView();
    }
}

// ارسال داده ویرایش شخص به API
async function handleEditPerson(event, id) {
    event.preventDefault();
    const name = document.getElementById("editPersonName").value;
    const family = document.getElementById("editPersonFamily").value;
    const nationalId = document.getElementById("editpersonNationalId").value;

    const response = await fetch('/api/Person', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: id, firstName: name, lastName: family, nationalId:nationalId })
    });

    if (response.ok) {
        loadPersonsView();
    } else {
        alert("خطا در ویرایش شخص!");
    }
}

// حذف شخص
async function deletePerson(id) {
    if (!confirm(`آیا از حذف شخص با شناسه ${id} مطمئن هستید؟`)) return;

    const response = await fetch(`/api/Person/${id}`, {
        method: 'DELETE'
    });

    if (response.ok) {
        loadPersonsView();
    } else {
        alert("خطا در حذف شخص!");
    }
}


//محصولات

// نمایش لیست محصولات

async function loadProductsView() {
    spaContainer.innerHTML = '<div class="text-center py-5"><div class="spinner-border text-primary" role="status"></div><p class="mt-2">در حال بارگذاری محصولات ...</p></div>';
    try {
        const response = await fetch('/api/Product');
        const products = await response.json(); 
        let rowsHtml = ''; 
        if (products.length === 0) {
            rowsHtml = '<tr><td colspan="4" class="text-center text-muted">هیچ محصولی ثبت نشده است.</td></tr>';
        } else {
            products.forEach(p => {
                rowsHtml +=`
                   <tr>
                        <td>${p.id}</td>
                        <td>${p.title}</td>
                        <td>${p.count}</td>
                        <td>${p.categoryTitle}</td>
                        <td>
                            <button class="btn btn-sm btn-outline-warning me-1" onclick="loadEditProductView(${p.id})">ویرایش</button>
                            <button class="btn btn-sm btn-outline-danger" onclick="deleteProduct(${p.id})">حذف</button>
                        </td>
                    </tr>
                `
            })
        }
        spaContainer.innerHTML = `
            <div class="card shadow-sm">
                <div class="card-header bg-info text-white d-flex justify-content-between align-items-center">
                    <h5 class="mb-0">لیست محصولات</h5>
                    <button class="btn btn-light btn-sm text-info fw-bold" onclick="loadCreateProductView()">+ ثبت محصول جدید</button>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-hover align-middle">
                            <thead class="table-light">
                                <tr>
                                    <th>شناسه</th>
                                    <th>نام محصول</th>
                                    <th>تعداد</th>
                                    <th>دسته‌بندی</th>
                                    <th style="width: 160px;">عملیات</th>
                                </tr>
                            </thead>
                            <tbody>
                                ${rowsHtml}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        `;
    } catch (err) {
        spaContainer.innerHTML = `<div class="alert alert-danger">خطا در بارگذاری لیست محصولات: ${err.message}</div>`;
    }

}

//دسته بندی 
async function getCategoriesOptions(selectedCategoryId = null) {
    try {
        const response = await fetch('/api/Product/GetCategories');
        const categories = await response.json();
        return categories.map(c =>
            `<option value="${c.id}" ${c.id === selectedCategoryId ? 'selected' : ''}>${c.title}</option>`
        ).join('');
    } catch (err) {
        return '<option value="">خطا در بارگذاری دسته‌ها</option>';
    }
}

// فرم محصولات

async function loadCreateProductView() {
    spaContainer.innerHTML = '<div class="text-center py-5"><div class="spinner-border text-info" role="status"></div></div>';

    const categoryOptions = await getCategoriesOptions();

    spaContainer.innerHTML = `
        <div class="card shadow-sm mx-auto" style="max-width: 500px;">
            <div class="card-header bg-success text-white">
                <h5 class="mb-0">ثبت محصول جدید</h5>
            </div>
            <div class="card-body">
                <form id="createProductForm" onsubmit="handleCreateProduct(event)">
                    <div class="mb-3">
                        <label class="form-label">نام محصول</label>
                        <input type="text" id="productName" class="form-control" required placeholder="مثال: لپ‌تاپ لنوو" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">تعداد</label>
                        <input type="number" id="productCount" class="form-control" required placeholder="مثال: 45" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">دسته‌بندی</label>
                        <select id="productCategoryId" class="form-select" required>
                            <option value="">-- انتخاب دسته‌بندی --</option>
                            ${categoryOptions}
                        </select>
                    </div>
                    <div class="d-flex justify-content-between">
                        <button type="button" class="btn btn-secondary" onclick="loadProductsView()">بازگشت</button>
                        <button type="submit" class="btn btn-success">ثبت نهایی</button>
                    </div>
                </form>
            </div>
        </div>
    `;
}

//ارسال محصول جدید 
async function handleCreateProduct(event) {
    event.preventDefault();
    try {
        const title = document.getElementById("productName").value;
        const count = parseInt(document.getElementById("productCount").value);
        const categoryId = parseInt(document.getElementById("productCategoryId").value);

        const response = await fetch('/api/Product', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title: title, count: count, categoryId: categoryId })
        });

      
        if (response.ok) {
            loadProductsView(); 

        } else {
alert("خطایی رخ داده است.")
        }
        
    }
     catch(err) {
      
        console.error("Validation Error:", err);
        alert("خطا در ثبت محصول! جزئیات در کنسول مرورگر بررسی شود.");
    }
}

//  فرم ویرایش محصول
async function loadEditProductView(id) {
    spaContainer.innerHTML = '<div class="text-center py-5"><div class="spinner-border text-warning" role="status"></div></div>';

    try {
        const response = await fetch(`/api/Product/${id}`);
        const product = await response.json();
        const categoryOptions = await getCategoriesOptions(product.categoryId);

        spaContainer.innerHTML = `
            <div class="card shadow-sm mx-auto" style="max-width: 500px;">
                <div class="card-header bg-warning text-dark">
                    <h5 class="mb-0">ویرایش محصول</h5>
                </div>
                <div class="card-body">
                    <form id="editProductForm" onsubmit="handleEditProduct(event, ${product.id})">
                        <div class="mb-3">
                            <label class="form-label">نام محصول</label>
                            <input type="text" id="editProductName" class="form-control" value="${product.title}" required />
                        </div>
                        <div class="mb-3">
                            <label class="form-label">تعداد</label>
                            <input type="number" id="editProductCount" class="form-control" value="${product.count}" required />
                        </div>
                        <div class="mb-3">
                            <label class="form-label">دسته‌بندی</label>
                            <select id="editProductCategoryId" class="form-select" required>
                                ${categoryOptions}
                            </select>
                        </div>
                        <div class="d-flex justify-content-between">
                            <button type="button" class="btn btn-secondary" onclick="loadProductsView()">انصراف</button>
                            <button type="submit" class="btn btn-warning">ذخیره تغییرات</button>
                        </div>
                    </form>
                </div>
            </div>
        `;
    } catch (err) {
        alert("خطا در بارگذاری مشخصات محصول!");
        loadProductsView();


    }
}

// ارسال داده ویرایش محصول به API
async function handleEditProduct(event, id) {
    event.preventDefault();
    try {
        const title = document.getElementById("editProductName").value;
        const count = parseInt(document.getElementById("editProductCount").value);
        const categoryId = parseInt(document.getElementById("editProductCategoryId").value);

        const response = await fetch('/api/Product', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: id, title: title, count: count, categoryId: categoryId })
        });
        if (response.ok) {

            loadProductsView();
        }
        else {
            alert("خطایی رخ داده است.")
        }
    }
    catch {
        alert("خطا در ویرایش محصول!");

    }
    
}

// ۴. حذف محصول
async function deleteProduct(id) {
    if (!confirm(`آیا از حذف محصول با شناسه ${id} مطمئن هستید؟`)) return;
    try {

        const response = await fetch(`/api/Product/${id}`, {
            method: 'DELETE'
        });
        if (response.ok) {
            loadProductsView()
        } else {
            alert("خطایی رخ داده است.")
        }
    }
    catch {
        alert("خطا در حذف محصول!");

    }
    
}
