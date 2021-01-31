INSERT INTO users (username, firstname, surname, address_type, street_address, suburb, city, postal_code)
VALUES  ('gvntri005', 'Trinesan', 'Govender', 'R', '7 Cotswold Drive', 'Shallcross', 'Durban', '4093'),
        ('gvnsue001', 'Suendharan', 'Govender', 'R', '8 Cotswold Drive', 'Shallcross', 'Durban', '4093'),
        ('gvnpre002', 'Prem', 'Govender', 'B', '9 Cotswold Drive', 'Shallcross', 'Durban', '4093');
																
INSERT INTO products (title, description, price, image_path)
VALUES  ('Pencil', 'aaa', 1.00, '/product_images/pencil.jpg'),
        ('Pen', 'qqq', 2.00, '/product_images/pen.jpg'),
        ('Erasor', 'www', 3.00, '/product_images/erasor.jpg'),
        ('Ruler', 'eee', 4.00, '/product_images/ruler.jpg'),
        ('Paper', 'sss', 5.00, '/product_images/paper.jpg'),
        ('Clipboard', 'ddd', 6.00, '/product_images/clipboard.jpg'),
        ('File', 'zzz', 7.00, '/product_images/file.jpg'),
        ('Book', 'xxx', 8.00, '/product_images/book.jpg'),
        ('Scissors', 'ccc', 9.00, '/product_images/scissors.jpg'),
        ('Tippex', 'vvv', 10.00, '/product_images/tippex.jpg');
																
INSERT INTO user_basket (user_id, product_id, quantity)
VALUES  (1, 1, 5),
		(1, 3, 7),
		(1, 5, 9);
					
COMMIT;