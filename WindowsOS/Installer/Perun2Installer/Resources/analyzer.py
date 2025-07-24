import sys
import ast
import traceback


SCRIPT_PATH = sys.argv[1]


def syntax_error(node, msg):
    source_line = open(SCRIPT_PATH).readlines()[node.lineno - 1].rstrip('\n')
    
    try:
        raise SyntaxError(msg,(SCRIPT_PATH, node.lineno, node.col_offset + 1, source_line))
    except SyntaxError as e:
        tb_text = ''.join(traceback.format_exception_only(type(e), e))
        print(tb_text, file=sys.stderr)
        sys.exit(1)


def get_main_function_node(file_path):
    with open(file_path, 'r') as file:
        tree = ast.parse(file.read(), filename=file_path)
    
    output = None
    
    for node in ast.walk(tree):
        if isinstance(node, ast.FunctionDef) and node.name == 'main':
            if output is not None:
                syntax_error(node, "the function \"main\" has been defined twice.")
            output = node
            
    return output


MAIN_NODE = get_main_function_node(SCRIPT_PATH)


if not MAIN_NODE:
    print("  The file \"" + SCRIPT_PATH + "\" does not contain the function \"main\".")
    print("")
    sys.exit(1)


def check_main_args(node):
    args = node.args
            
    if args.vararg is not None:
        syntax_error(node, "the function \"main\" cannot contain \"*args\".")
            
    if args.kwarg is not None:
        syntax_error(node, "the function \"main\" cannot contain \"**kwargs\".")
            
    if len(args.kwonlyargs) > 0:
        syntax_error(node, "the function \"main\" cannot contain keyword-only arguments.")
            
    if hasattr(args, 'posonlyargs') and len(args.posonlyargs) > 0:
        syntax_error(node, "the function \"main\" cannot contain positional-only arguments.")
            
    if len(args.args) != 1 or args.args[0].arg != 'file_name':
        syntax_error(node, "the function \"main\" must contain exactly one argument and it should be named \"file_name\".")

    return False


check_main_args(MAIN_NODE)


class ReturnBoolChecker(ast.NodeVisitor):
    def __init__(self):
        self.returns = []
    
    def visit_Return(self, node):
        self.returns.append((node.value, node.lineno, node.col_offset))
        self.generic_visit(node)


class FakeNode(ast.AST):
    def __init__(self, lineno, col_offset):
        self.lineno = lineno
        self.col_offset = col_offset


def check_returns_boolean(node):
    checker = ReturnBoolChecker()
    checker.visit(node)

    for value, lineno, col_offset in checker.returns:
        if value is None:
            obj = FakeNode(lineno, col_offset)
            syntax_error(obj, "this return has no value. The function \"main\" must always return a Boolean.")
        elif isinstance(value, ast.Constant) and not isinstance(value.value, bool):
            syntax_error(value, "this return value has the wrong type. The function \"main\" must always return a Boolean.")


check_returns_boolean(MAIN_NODE)


def always_returns_value(statements):
    for stat in statements:
        if isinstance(stat, ast.Return):
            return True

        elif isinstance(stat, ast.If):
            then_returns = always_returns_value(stat.body)
            else_returns = always_returns_value(stat.orelse) if stat.orelse else False
            if then_returns and else_returns:
                return True
            else:
                continue

        elif isinstance(stat, (ast.FunctionDef, ast.ClassDef)):
            continue

        else:
            continue

    return False


if not always_returns_value(MAIN_NODE.body):
    syntax_error(MAIN_NODE, ("the function \"main\" seems to not always return a value. "
        + "Please add missing return statements."))
