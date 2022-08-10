# insert category
Invoke-RestMethod http://localhost:5252/Category/add -Method POST -Body (@{name = "First category"; image = 'http://google.com';} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"
Invoke-RestMethod http://localhost:5252/Category/add -Method POST -Body (@{name = "Not updated category"; image = 'url_5'; parentCategoryId = 1} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"

# update category
Invoke-RestMethod http://localhost:5252/Category/update -Method PATCH -Body (@{id = 2; name = "Second category"; image = 'http://google.com'; parentCategoryId = 1} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"

# list categories
Invoke-RestMethod http://localhost:5252/Category/list -Method GET

# insert Item
Invoke-RestMethod http://localhost:5252/Item/add -Method POST -Body (@{name = "First Item"; description="Firts Item description"; image = 'http://google.com'; categoryId=1; price=100; amount = 15} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"
Invoke-RestMethod http://localhost:5252/Item/add -Method POST -Body (@{name = "Not Updated Item"; description="not updated Item description"; image = '123'; categoryId=1; price=200; amount = 100} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"

#update item
Invoke-RestMethod http://localhost:5252/Item/update -Method PATCH -Body (@{id = 2; name = "Second Item"; description="Second Item description"; image = 'http://google.com'; categoryId=2; price=2; amount = 1} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"

#list items
Invoke-RestMethod http://localhost:5252/Item/list -Method GET

#delete category
Invoke-RestMethod http://localhost:5252/Category/delete?id=2 -Method Delete

#delete item
Invoke-RestMethod http://localhost:5252/Item/delete?id=1 -Method Delete